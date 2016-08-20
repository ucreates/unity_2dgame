using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
public sealed class RankingCanvasBehaviour  : BaseBehaviour, IStateMachine<RankingCanvasBehaviour>, INotify {
    public List<Sprite> scoreSpriteList;
    public FiniteStateMachine<RankingCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<RankingCanvasBehaviour>(this);
        this.stateMachine.Add("show", new RankingCanvasShowState());
        this.stateMachine.Add("stay", new RankingCanvasStayState());
        this.stateMachine.Add("hide", new RankingCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.RankingShow) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.RankingHide) {
            this.stateMachine.Change("hide");
        }
    }
}
