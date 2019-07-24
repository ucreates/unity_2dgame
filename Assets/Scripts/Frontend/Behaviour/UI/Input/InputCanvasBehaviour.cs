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
using Frontend.Behaviour.Base;
using Frontend.Behaviour.State;
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UnityEngine;
using UnityEngine.UI;
public sealed class InputCanvasBehaviour : BaseBehaviour, IStateMachine<InputCanvasBehaviour>, INotify, IValidator, IInputUIAsset {
    public FiniteStateMachine<InputCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<InputCanvasBehaviour>(this);
        this.stateMachine.Add("show", new InputCanvasShowState());
        this.stateMachine.Add("stay", new InputCanvasStayState());
        this.stateMachine.Add("hide", new InputCanvasHideState());
        this.stateMachine.Add("error", new InputCanvasErrorState());
        this.stateMachine.Change("show");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(int notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.InputProfileError) {
            this.stateMachine.Change("error", parameter);
        } else if (notifyMessage == NotifyMessage.InputProfile) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.GameTitle) {
            this.stateMachine.Change("hide");
        }
    }
    public ValidatorResponse IsValid() {
        Parameter parameter = this.GetInput();
        ValidatorGateway validator = ValidatorGateway.GetInstance();
        ValidatorResponse res = validator.IsValid(parameter);
        return res;
    }
    public Parameter GetInput() {
        Parameter parmeter = new Parameter();
        GameObject nickName = GameObject.Find("InputCanvas/ModalDialog/NickNameInputField");
        if (null != nickName) {
            string nntxt = nickName.GetComponent<InputField>().text.Trim();
            parmeter.Set<string>("nickname", nntxt);
        }
        GameObject password = GameObject.Find("InputCanvas/ModalDialog/PasswordInputField");
        if (null != password) {
            string pwdtxt = password.GetComponent<InputField>().text.Trim();
            parmeter.Set<string>("password", pwdtxt);
        }
        GameObject mailphone = GameObject.Find("InputCanvas/ModalDialog/PhoneNumberOrMailAddressInputField");
        if (null != mailphone) {
            string mptxt = mailphone.GetComponent<InputField>().text.Trim();
            parmeter.Set<string>("mailphone", mptxt);
        }
        return parmeter;
    }
}
