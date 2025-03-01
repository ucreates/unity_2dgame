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

public sealed class CameraBehaviour : BaseBehaviour, IStateMachine<CameraBehaviour>, INotify
{
    public const float DEFAULT_OTHROGRAPHIC_SIZE = 5.0f;

    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.FlappyBirdDead; })?.Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<CameraBehaviour>(this);
        stateMachine?.Add("stop", new CameraStopState());
        stateMachine?.Add("shake", new CameraShakeState());
        stateMachine?.Change("stop");
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.FlappyBirdDead)
            stateMachine?.Change("shake");
    }

    public FiniteStateMachine<CameraBehaviour> stateMachine { get; set; }
}