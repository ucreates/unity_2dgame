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

public sealed class PlayCanvasBehaviour : BaseBehaviour, IStateMachine<PlayCanvasBehaviour>, INotify
{
    public List<Sprite> scoreSpriteList;

    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<PlayCanvasBehaviour>(this);
        stateMachine.Add("show", new PlayCanvasShowState());
        stateMachine.Add("hide", new PlayCanvasHideState());
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
        if (notifyMessage == NotifyMessage.GameStart)
            stateMachine.Change("show");
        else
            stateMachine.Change("hide");
    }

    public FiniteStateMachine<PlayCanvasBehaviour> stateMachine { get; set; }
}