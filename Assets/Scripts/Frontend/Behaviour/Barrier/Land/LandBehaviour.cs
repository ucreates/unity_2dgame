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
using Frontend.Component.Asset.Render;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
public sealed class LandBehaviour : BaseBehaviour, IStateMachine<LandBehaviour>, INotify {
    public const float UV_SCROLL_RATE = 0.75f;
    public FiniteStateMachine<LandBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.assetCollection.Set("anime", new MaterialAsset(this));
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<LandBehaviour>(this);
        this.stateMachine.Add("scroll", new LandScrollState());
        this.stateMachine.Add("stop", new LandStopState());
        this.stateMachine.Change("scroll");
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
        } else if (notifyMessage == NotifyMessage.GameStart) {
            this.stateMachine.Change("scroll");
        }
    }
}
