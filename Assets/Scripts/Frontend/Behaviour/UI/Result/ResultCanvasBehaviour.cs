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
using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UnityEngine;

public sealed class ResultCanvasBehaviour : BaseBehaviour, IStateMachine<ResultCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;
    public List<Sprite> emblemSpriteList;

    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ResultCanvasBehaviour>(this);
        stateMachine.Add("show", new ResultCanvasShowState());
        stateMachine.Add("hide", new ResultCanvasHideState());
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
        if (notifyMessage == NotifyMessage.GameOver)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.GameRestart || notifyMessage == NotifyMessage.RankingShow)
            stateMachine.Change("hide");
    }

    public FiniteStateMachine<ResultCanvasBehaviour> stateMachine { get; set; }
}