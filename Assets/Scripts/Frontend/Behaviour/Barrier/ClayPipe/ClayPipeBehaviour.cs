//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;

public sealed class ClayPipeBehaviour : BaseBehaviour, IStateMachine<ClayPipeBehaviour>, INotify
{
    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify().Where(message => { return message.title == NotifyMessage.Title.FlappyBirdDead || message.title == NotifyMessage.Title.GameRestart; }).Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ClayPipeBehaviour>(this);
        stateMachine.Add("move", new ClayPipeMoveState());
        stateMachine.Add("destroy", new ClayPipeDestroyState());
        stateMachine.Add("stop", new ClayPipeStopState());
        stateMachine.Change("move");
        stateMachine.Play();
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage.title == NotifyMessage.Title.FlappyBirdDead)
            stateMachine.Change("stop");
        else if (notifyMessage.title == NotifyMessage.Title.GameRestart) 
            stateMachine.Change("destroy");
    }

    public FiniteStateMachine<ClayPipeBehaviour> stateMachine { get; set; }
}