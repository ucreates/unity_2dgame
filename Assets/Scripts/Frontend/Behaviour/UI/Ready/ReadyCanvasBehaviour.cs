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
using Service;
using Frontend.Notify;
using Frontend.Component.State;
using Frontend.Component.Property;
using Frontend.Behaviour.State;
using Core.Entity;
public sealed class ReadyCanvasBehaviour : BaseBehaviour, IStateMachine<ReadyCanvasBehaviour>, INotify {
    public FiniteStateMachine<ReadyCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<ReadyCanvasBehaviour>(this);
        this.stateMachine.Add("show", new ReadyCanvasShowState());
        this.stateMachine.Add("hide", new ReadyCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameReady) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.GameStart) {
            this.stateMachine.Change("hide");
        } else if (notifyMessage == NotifyMessage.GameRestart) {
            this.stateMachine.Change("show");
        }
    }
}
