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

public sealed class CurtainBehaviour : BaseBehaviour, IStateMachine<CurtainBehaviour>, INotify
{
    // Use this for initialization
    private void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<CurtainBehaviour>(this);
        stateMachine.Add("blink", new CurtainBlinkState());
        stateMachine.Add("show", new CurtainShowState());
        stateMachine.Add("destroy", new CurtainDestroyState());
        stateMachine.Change("blink");
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
        if (notifyMessage == NotifyMessage.GameRestart)
            stateMachine.Change("destroy");
        else if (notifyMessage == NotifyMessage.GameOver) stateMachine.Change("show");
    }

    public FiniteStateMachine<CurtainBehaviour> stateMachine { get; set; }
}