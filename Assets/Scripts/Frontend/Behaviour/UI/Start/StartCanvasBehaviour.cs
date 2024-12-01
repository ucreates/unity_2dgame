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

public sealed class StartCanvasBehaviour : BaseBehaviour, IStateMachine<StartCanvasBehaviour>, INotify
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<StartCanvasBehaviour>(this);
        stateMachine.Add("show", new StartCanvasShowState());
        stateMachine.Add("hide", new StartCanvasHideState());
        stateMachine.Change("hide");
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
        if (notifyMessage == NotifyMessage.GameTitle)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.GameReady || notifyMessage == NotifyMessage.RegulationShow ||
                 notifyMessage == NotifyMessage.RankingShow ||
                 notifyMessage == NotifyMessage.ShopShow) stateMachine.Change("hide");
    }

    public FiniteStateMachine<StartCanvasBehaviour> stateMachine { get; set; }
}