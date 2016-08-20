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
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using Service;
public sealed class PlayCanvasBehaviour : BaseBehaviour, IStateMachine<PlayCanvasBehaviour>, INotify {
    public List<Sprite> scoreSpriteList;
    public FiniteStateMachine<PlayCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<PlayCanvasBehaviour>(this);
        this.stateMachine.Add("show", new PlayCanvasShowState());
        this.stateMachine.Add("hide", new PlayCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameStart) {
            this.stateMachine.Change("show");
        } else {
            this.stateMachine.Change("hide");
        }
    }
}
