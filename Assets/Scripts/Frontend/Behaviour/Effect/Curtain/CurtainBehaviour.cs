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

public sealed class CurtainBehaviour : BaseBehaviour, IStateMachine<CurtainBehaviour>, INotify
{
    // Use this for initialization
    private void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.GameRestart || message.title == NotifyMessage.Title.GameOver; })?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<CurtainBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<CurtainBehaviour>>
        {
            { "blink", new CurtainBlinkState() },
            { "show", new CurtainShowState() },
            { "destroy", new CurtainDestroyState() }
        });
        stateMachine?.Change("blink");
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.GameRestart)
            stateMachine?.Change("destroy");
        else if (notifyMessage?.title == NotifyMessage.Title.GameOver)
            stateMachine?.Change("show");
    }

    public FiniteStateMachine<CurtainBehaviour> stateMachine { get; set; }
}