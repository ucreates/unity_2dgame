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
using Frontend.Notify;
public sealed class StartCanvasBehaviour : BaseBehaviour, IStateMachine<StartCanvasBehaviour>, INotify {
    public FiniteStateMachine<StartCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<StartCanvasBehaviour>(this);
        this.stateMachine.Add("show", new StartCanvasShowState());
        this.stateMachine.Add("hide", new StartCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    // Update is called once per frame
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameTitle) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.GameReady || notifyMessage == NotifyMessage.RegulationShow || notifyMessage == NotifyMessage.RankingShow || notifyMessage == NotifyMessage.ShopShow) {
            this.stateMachine.Change("hide");
        }
    }
}
