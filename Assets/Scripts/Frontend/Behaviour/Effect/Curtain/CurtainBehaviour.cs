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
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Notify;
public sealed class CurtainBehaviour : BaseBehaviour, IStateMachine<CurtainBehaviour>, INotify {
    public FiniteStateMachine<CurtainBehaviour> stateMachine {
        get;
        set;
    }
    // Use this for initialization
    void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<CurtainBehaviour>(this);
        this.stateMachine.Add("blink", new CurtainBlinkState());
        this.stateMachine.Add("show", new CurtainShowState());
        this.stateMachine.Add("destroy", new CurtainDestroyState());
        this.stateMachine.Change("blink");
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    // Update is called once per frame
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameRestart) {
            this.stateMachine.Change("destroy");
        } else if (notifyMessage == NotifyMessage.GameOver) {
            this.stateMachine.Change("show");
        }
    }
}
