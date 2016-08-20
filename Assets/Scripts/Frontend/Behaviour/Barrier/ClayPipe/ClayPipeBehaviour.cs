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
using Service;
public sealed class ClayPipeBehaviour : BaseBehaviour, IStateMachine<ClayPipeBehaviour>, INotify {
    public FiniteStateMachine<ClayPipeBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<ClayPipeBehaviour>(this);
        this.stateMachine.Add("move", new ClayPipeMoveState());
        this.stateMachine.Add("destroy", new ClayPipeDestroyState());
        this.stateMachine.Add("stop", new ClayPipeStopState());
        this.stateMachine.Change("move");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.FlappyBirdDead) {
            this.stateMachine.Change("stop");
        } else if (notifyMessage == NotifyMessage.GameRestart) {
            this.stateMachine.Change("destroy");
        }
    }
}
