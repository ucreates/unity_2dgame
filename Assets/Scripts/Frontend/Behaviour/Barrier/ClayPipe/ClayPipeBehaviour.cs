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

public sealed class ClayPipeBehaviour : BaseBehaviour, IStateMachine<ClayPipeBehaviour>, INotify
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ClayPipeBehaviour>(this);
        stateMachine.Add("move", new ClayPipeMoveState());
        stateMachine.Add("destroy", new ClayPipeDestroyState());
        stateMachine.Add("stop", new ClayPipeStopState());
        stateMachine.Change("move");
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
        if (notifyMessage == NotifyMessage.FlappyBirdDead)
            stateMachine.Change("stop");
        else if (notifyMessage == NotifyMessage.GameRestart) stateMachine.Change("destroy");
    }

    public FiniteStateMachine<ClayPipeBehaviour> stateMachine { get; set; }
}