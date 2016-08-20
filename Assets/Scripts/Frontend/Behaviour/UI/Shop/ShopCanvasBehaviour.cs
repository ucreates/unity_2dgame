//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using Core.Entity;
using Frontend.Behaviour.State.UI.Shop;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
public sealed class ShopCanvasBehaviour : BaseBehaviour, IStateMachine<ShopCanvasBehaviour>, INotify {
    public FiniteStateMachine<ShopCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<ShopCanvasBehaviour>(this);
        this.stateMachine.Add("listshow", new ListModalDialogShowState());
        this.stateMachine.Add("liststay", new ListModalDialogStayState());
        this.stateMachine.Add("listhide", new ListModalDialogHideState());
        this.stateMachine.Add("confirmshow", new ConfirmModalDialogShowState());
        this.stateMachine.Add("confirmstay", new ConfirmModalDialogStayState());
        this.stateMachine.Add("commitshow", new CommitModalDialogShowState());
        this.stateMachine.Add("commitstay", new CommitModalDialogStayState());
        this.stateMachine.Change("listhide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    // Update is called once per frame
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.ShopShow) {
            this.stateMachine.Change("listshow");
        } else if (notifyMessage == NotifyMessage.ShopHide) {
            this.stateMachine.Change("listhide");
        } else if (notifyMessage == NotifyMessage.ShopCommitShow) {
            this.stateMachine.Change("commitshow", parameter);
        } else if (notifyMessage == NotifyMessage.ShopConfirmShow) {
            this.stateMachine.Change("confirmshow", parameter);
        }
    }
}
