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
using Frontend.Component.Asset.Render;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;

public sealed class LandBehaviour : BaseBehaviour, IStateMachine<LandBehaviour>, INotify
{
    public const float UV_SCROLL_RATE = 0.75f;

    public void Start()
    {
        assetCollection.Set("anime", new MaterialAsset(this));
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<LandBehaviour>(this);
        stateMachine.Add("scroll", new LandScrollState());
        stateMachine.Add("stop", new LandStopState());
        stateMachine.Change("scroll");
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
        else if (notifyMessage == NotifyMessage.GameStart) stateMachine.Change("scroll");
    }

    public FiniteStateMachine<LandBehaviour> stateMachine { get; set; }
}