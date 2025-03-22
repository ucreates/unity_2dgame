//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Frontend.Behaviour.State;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;

public sealed class StartCanvasBehaviour : BaseBehaviour, IStateMachine<StartCanvasBehaviour>, INotify
{
    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.GameTitle || message.title == NotifyMessage.Title.GameReady || message.title == NotifyMessage.Title.RegulationShow || message.title == NotifyMessage.Title.RankingShow || message.title == NotifyMessage.Title.ShopShow; })?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<StartCanvasBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<StartCanvasBehaviour>>
        {
            { "show", new StartCanvasShowState() },
            { "hide", new StartCanvasHideState() }
        });
        stateMachine?.Change("hide");
        stateMachine?.Play();
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.GameTitle)
            stateMachine?.Change("show");
        else if (notifyMessage?.title == NotifyMessage.Title.GameReady || notifyMessage?.title == NotifyMessage.Title.RegulationShow ||
                 notifyMessage?.title == NotifyMessage.Title.RankingShow ||
                 notifyMessage?.title == NotifyMessage.Title.ShopShow)
            stateMachine?.Change("hide");
    }

    public FiniteStateMachine<StartCanvasBehaviour> stateMachine { get; set; }
}