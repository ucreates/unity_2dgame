//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Core.Validator;
using Core.Validator.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UnityEngine;
using UnityEngine.UI;

public sealed class InputCanvasBehaviour : BaseBehaviour, IStateMachine<InputCanvasBehaviour>, INotify, IValidator,
    IInputUIAsset
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<InputCanvasBehaviour>(this);
        stateMachine.Add("show", new InputCanvasShowState());
        stateMachine.Add("stay", new InputCanvasStayState());
        stateMachine.Add("hide", new InputCanvasHideState());
        stateMachine.Add("error", new InputCanvasErrorState());
        stateMachine.Change("show");
        stateMachine.Play();
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public Parameter GetInput()
    {
        var parmeter = new Parameter();
        var nickName = GameObject.Find("InputCanvas/ModalDialog/NickNameInputField");
        if (null != nickName)
        {
            var nntxt = nickName.GetComponent<InputField>().text.Trim();
            parmeter.Set("nickname", nntxt);
        }

        var password = GameObject.Find("InputCanvas/ModalDialog/PasswordInputField");
        if (null != password)
        {
            var pwdtxt = password.GetComponent<InputField>().text.Trim();
            parmeter.Set("password", pwdtxt);
        }

        var mailphone = GameObject.Find("InputCanvas/ModalDialog/PhoneNumberOrMailAddressInputField");
        if (null != mailphone)
        {
            var mptxt = mailphone.GetComponent<InputField>().text.Trim();
            parmeter.Set("mailphone", mptxt);
        }

        return parmeter;
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.InputProfileError)
            stateMachine.Change("error", parameter);
        else if (notifyMessage == NotifyMessage.InputProfile)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.GameTitle) stateMachine.Change("hide");
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