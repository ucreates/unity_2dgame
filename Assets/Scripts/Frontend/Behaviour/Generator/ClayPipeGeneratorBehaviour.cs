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

public sealed class ClayPipeGeneratorBehaviour : BaseBehaviour, IStateMachine<ClayPipeGeneratorBehaviour>, INotify
{
    // Use this for initialization
    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.FlappyBirdDead || message.title == NotifyMessage.Title.GameStart; })?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<ClayPipeGeneratorBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<ClayPipeGeneratorBehaviour>>
        {
            { "generate", new ClayPipeGeneratorGenerateState() },
            { "stop", new ClayPipeGeneratorStopState() }
        });
        stateMachine?.Change("stop");
        stateMachine?.Play();
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.FlappyBirdDead)
            stateMachine?.Change("stop");
        else if (notifyMessage?.title == NotifyMessage.Title.GameStart)
            stateMachine?.Change("generate");
    }

    public FiniteStateMachine<ClayPipeGeneratorBehaviour> stateMachine { get; set; }
}