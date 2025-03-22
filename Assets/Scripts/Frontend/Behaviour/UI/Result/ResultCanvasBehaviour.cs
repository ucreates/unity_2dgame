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
using UnityEngine;

public sealed class ResultCanvasBehaviour : BaseBehaviour, IStateMachine<ResultCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;
    public List<Sprite> emblemSpriteList;

    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.GameOver || message.title == NotifyMessage.Title.GameRestart || message.title == NotifyMessage.Title.RankingShow; })?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<ResultCanvasBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<ResultCanvasBehaviour>>
        {
            { "show", new ResultCanvasShowState() },
            { "hide", new ResultCanvasHideState() }
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
        if (notifyMessage?.title == NotifyMessage.Title.GameOver)
            stateMachine?.Change("show");
        else if (notifyMessage?.title == NotifyMessage.Title.GameRestart || notifyMessage?.title == NotifyMessage.Title.RankingShow)
            stateMachine?.Change("hide");
    }

    public FiniteStateMachine<ResultCanvasBehaviour> stateMachine { get; set; }
}