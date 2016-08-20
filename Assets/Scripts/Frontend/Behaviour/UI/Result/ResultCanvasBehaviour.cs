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
using System.Collections.Generic;
using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
public sealed class ResultCanvasBehaviour : BaseBehaviour, IStateMachine<ResultCanvasBehaviour>, INotify {
    public List<Sprite> scoreSpriteList;
    public List<Sprite> emblemSpriteList;
    public FiniteStateMachine<ResultCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<ResultCanvasBehaviour>(this);
        this.stateMachine.Add("show", new ResultCanvasShowState());
        this.stateMachine.Add("hide", new ResultCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameOver) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.GameRestart || notifyMessage == NotifyMessage.RankingShow) {
            this.stateMachine.Change("hide");
        }
    }
}
