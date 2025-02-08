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

public sealed class ReadyCanvasBehaviour : BaseBehaviour, IStateMachine<ReadyCanvasBehaviour>, INotify
{
    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify().Where(message => { return message.title == NotifyMessage.Title.GameReady || message.title == NotifyMessage.Title.GameStart || message.title == NotifyMessage.Title.GameRestart; }).Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ReadyCanvasBehaviour>(this);
        stateMachine.Add("show", new ReadyCanvasShowState());
        stateMachine.Add("hide", new ReadyCanvasHideState());
        stateMachine.Change("hide");
        stateMachine.Play();
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage.title == NotifyMessage.Title.GameReady)
            stateMachine.Change("show");
        else if (notifyMessage.title == NotifyMessage.Title.GameStart)
            stateMachine.Change("hide");
        else if (notifyMessage.title == NotifyMessage.Title.GameRestart) stateMachine.Change("show");
    }

    public FiniteStateMachine<ReadyCanvasBehaviour> stateMachine { get; set; }
}