using System.Collections.Generic;
using Frontend.Behaviour.State;
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
        stateMachine = new FiniteStateMachine<RankingCanvasBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<RankingCanvasBehaviour>>
        {
            { "show", new RankingCanvasShowState() },
            { "stay", new RankingCanvasStayState() },
            { "hide", new RankingCanvasHideState() }
        });
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