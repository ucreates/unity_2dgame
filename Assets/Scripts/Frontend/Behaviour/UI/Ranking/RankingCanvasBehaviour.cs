using System.Collections.Generic;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;
using UnityEngine;

public sealed class RankingCanvasBehaviour : BaseBehaviour, IStateMachine<RankingCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;

    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.RankingShow || message.title == NotifyMessage.Title.RankingHide; })?.Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<RankingCanvasBehaviour>(this);
        stateMachine?.Add("show", new RankingCanvasShowState());
        stateMachine?.Add("stay", new RankingCanvasStayState());
        stateMachine?.Add("hide", new RankingCanvasHideState());
        stateMachine?.Change("hide");
        stateMachine?.Play();
    }

    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.RankingShow)
            stateMachine?.Change("show");
        else if (notifyMessage?.title == NotifyMessage.Title.RankingHide)
            stateMachine?.Change("hide");
    }

    public FiniteStateMachine<RankingCanvasBehaviour> stateMachine { get; set; }
}