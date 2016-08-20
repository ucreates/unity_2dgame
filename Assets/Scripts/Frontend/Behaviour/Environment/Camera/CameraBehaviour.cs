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
public sealed class CameraBehaviour : BaseBehaviour, IStateMachine<CameraBehaviour>, INotify {
    public const float DEFAULT_OTHROGRAPHIC_SIZE = 5.0f;
    public FiniteStateMachine<CameraBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<CameraBehaviour>(this);
        this.stateMachine.Add("stop", new CameraStopState());
        this.stateMachine.Add("shake", new CameraShakeState());
        this.stateMachine.Change("stop");
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    // Update is called once per frame
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.FlappyBirdDead) {
            this.stateMachine.Change("shake");
        }
    }
}
