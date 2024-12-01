//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;

public sealed class ReadyCanvasBehaviour : BaseBehaviour, IStateMachine<ReadyCanvasBehaviour>, INotify
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ReadyCanvasBehaviour>(this);
        stateMachine.Add("show", new ReadyCanvasShowState());
        stateMachine.Add("hide", new ReadyCanvasHideState());
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
        if (notifyMessage == NotifyMessage.GameReady)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.GameStart)
            stateMachine.Change("hide");
        else if (notifyMessage == NotifyMessage.GameRestart) stateMachine.Change("show");
    }

    public FiniteStateMachine<ReadyCanvasBehaviour> stateMachine { get; set; }
}