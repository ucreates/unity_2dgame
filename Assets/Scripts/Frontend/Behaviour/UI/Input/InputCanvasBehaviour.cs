//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Validator;
using Core.Validator.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public sealed class InputCanvasBehaviour : BaseBehaviour, IStateMachine<InputCanvasBehaviour>, INotify, IValidator,
    IInputUIAsset
{
    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify().Where(message => { return message.title == NotifyMessage.Title.InputProfileError || message.title == NotifyMessage.Title.InputProfile || message.title == NotifyMessage.Title.GameTitle; }).Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<InputCanvasBehaviour>(this);
        stateMachine.Add("show", new InputCanvasShowState());
        stateMachine.Add("stay", new InputCanvasStayState());
        stateMachine.Add("hide", new InputCanvasHideState());
        stateMachine.Add("error", new InputCanvasErrorState());
        stateMachine.Change("show");
        stateMachine.Play();
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public object GetInput()
    {
        var input = new Dictionary<string, object>();
        var nickNameObject = GameObject.Find("InputCanvas/ModalDialog/NickNameInputField");
        if (null != nickNameObject)
        {
            var nickName = nickNameObject.GetComponent<InputField>().text.Trim();
            input.Add("nickName", nickName);
        }

        var passwordObject = GameObject.Find("InputCanvas/ModalDialog/PasswordInputField");
        if (null != passwordObject)
        {
            var password = passwordObject.GetComponent<InputField>().text.Trim();
            input.Add("password", password);
        }

        var mailphoneObject = GameObject.Find("InputCanvas/ModalDialog/PhoneNumberOrMailAddressInputField");
        if (null != mailphoneObject)
        {
            var mailPhone = mailphoneObject.GetComponent<InputField>().text.Trim();
            input.Add("mailPhone", mailPhone);
        }

        var maleToggleObject = GameObject.Find("InputCanvas/ModalDialog/MaleToggle");
        var femaleToggleObject = GameObject.Find("InputCanvas/ModalDialog/FemaleToggle");
        if (null != maleToggleObject && null != femaleToggleObject)
        {
            var gender = 0;
            if (maleToggleObject.GetComponent<Toggle>().isOn) gender = 1;
            else if (femaleToggleObject.GetComponent<Toggle>().isOn) gender = 2;
            input.Add("gender", gender);
        }

        return input;
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage.title == NotifyMessage.Title.InputProfileError)
            stateMachine.Change("error", notifyMessage.parameter);
        else if (notifyMessage.title == NotifyMessage.Title.InputProfile)
            stateMachine.Change("show");
        else if (notifyMessage.title == NotifyMessage.Title.GameTitle) stateMachine.Change("hide");
    }

    public FiniteStateMachine<InputCanvasBehaviour> stateMachine { get; set; }

    public ValidatorResponse IsValid()
    {
        var parameter = GetInput();
        var validator = ValidatorGateway.GetInstance();
        var res = validator.IsValid(parameter);
        return res;
    }
}