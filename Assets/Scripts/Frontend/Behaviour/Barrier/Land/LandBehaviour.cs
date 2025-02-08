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
using Frontend.Component.Asset.Render;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;

public sealed class LandBehaviour : BaseBehaviour, IStateMachine<LandBehaviour>, INotify
{
    public const float UV_SCROLL_RATE = 0.75f;

    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify().Where(message => { return message.title == NotifyMessage.Title.FlappyBirdDead || message.title == NotifyMessage.Title.GameStart; }).Subscribe(message => { OnNotify(message); });
        assetCollection.Set("anime", new MaterialAsset(this));
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<LandBehaviour>(this);
        stateMachine.Add("scroll", new LandScrollState());
        stateMachine.Add("stop", new LandStopState());
        stateMachine.Change("scroll");
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
        else if (notifyMessage.title == NotifyMessage.Title.GameStart) 
            stateMachine.Change("scroll");
    }

    public FiniteStateMachine<LandBehaviour> stateMachine { get; set; }
}