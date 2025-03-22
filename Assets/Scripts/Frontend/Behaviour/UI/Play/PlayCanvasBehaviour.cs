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

public sealed class PlayCanvasBehaviour : BaseBehaviour, IStateMachine<PlayCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;

    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify()?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<PlayCanvasBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<PlayCanvasBehaviour>>
        {
            { "show", new PlayCanvasShowState() },
            { "hide", new PlayCanvasHideState() }
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
        if (notifyMessage?.title == NotifyMessage.Title.GameStart)
            stateMachine?.Change("show");
        else
            stateMachine?.Change("hide");
    }

    public FiniteStateMachine<PlayCanvasBehaviour> stateMachine { get; set; }
}