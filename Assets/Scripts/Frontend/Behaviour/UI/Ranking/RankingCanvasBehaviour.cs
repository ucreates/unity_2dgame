using System.Collections.Generic;
using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UnityEngine;

public sealed class RankingCanvasBehaviour : BaseBehaviour, IStateMachine<RankingCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;

    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<RankingCanvasBehaviour>(this);
        stateMachine.Add("show", new RankingCanvasShowState());
        stateMachine.Add("stay", new RankingCanvasStayState());
        stateMachine.Add("hide", new RankingCanvasHideState());
        stateMachine.Change("hide");
        stateMachine.Play();
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.RankingShow)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.RankingHide)
            stateMachine.Change("hide");
    }

    public FiniteStateMachine<RankingCanvasBehaviour> stateMachine { get; set; }
}