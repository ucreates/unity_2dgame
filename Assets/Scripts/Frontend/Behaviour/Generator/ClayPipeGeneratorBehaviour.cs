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

public sealed class ClayPipeGeneratorBehaviour : BaseBehaviour, IStateMachine<ClayPipeGeneratorBehaviour>, INotify
{
    // Use this for initialization
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ClayPipeGeneratorBehaviour>(this);
        stateMachine.Add("generate", new ClayPipeGeneratorGenerateState());
        stateMachine.Add("stop", new ClayPipeGeneratorStopState());
        stateMachine.Change("stop");
        stateMachine.Play();
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.FlappyBirdDead)
            stateMachine.Change("stop");
        else if (notifyMessage == NotifyMessage.GameStart) stateMachine.Change("generate");
    }

    public FiniteStateMachine<ClayPipeGeneratorBehaviour> stateMachine { get; set; }
}