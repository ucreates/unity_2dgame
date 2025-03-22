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
using Frontend.Behaviour.State.UI.Shop;
using Frontend.Component.State;
using Frontend.Notify;
using UniRx;

public sealed class ShopCanvasBehaviour : BaseBehaviour, IStateMachine<ShopCanvasBehaviour>, INotify
{
    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.ShopShow || message.title == NotifyMessage.Title.ShopHide || message.title == NotifyMessage.Title.ShopCommitShow || message.title == NotifyMessage.Title.ShopConfirmShow; })?.Subscribe(message => { OnNotify(message); });
        stateMachine = new FiniteStateMachine<ShopCanvasBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<ShopCanvasBehaviour>>
        {
            { "listshow", new ListModalDialogShowState() },
            { "liststay", new ListModalDialogStayState() },
            { "listhide", new ListModalDialogHideState() },
            { "confirmshow", new ConfirmModalDialogShowState() },
            { "confirmstay", new ConfirmModalDialogStayState() },
            { "commitshow", new CommitModalDialogShowState() },
            { "commitstay", new CommitModalDialogStayState() }
        });
        stateMachine?.Change("listhide");
        stateMachine?.Play();
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine?.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.ShopShow)
            stateMachine?.Change("listshow");
        else if (notifyMessage?.title == NotifyMessage.Title.ShopHide)
            stateMachine?.Change("listhide");
        else if (notifyMessage?.title == NotifyMessage.Title.ShopCommitShow)
            stateMachine?.Change("commitshow", notifyMessage.parameter);
        else if (notifyMessage?.title == NotifyMessage.Title.ShopConfirmShow)
            stateMachine?.Change("confirmshow", notifyMessage.parameter);
    }

    public FiniteStateMachine<ShopCanvasBehaviour> stateMachine { get; set; }
}