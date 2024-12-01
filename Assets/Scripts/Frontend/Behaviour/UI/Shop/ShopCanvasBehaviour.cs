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
using Frontend.Behaviour.State.UI.Shop;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;

public sealed class ShopCanvasBehaviour : BaseBehaviour, IStateMachine<ShopCanvasBehaviour>, INotify
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<ShopCanvasBehaviour>(this);
        stateMachine.Add("listshow", new ListModalDialogShowState());
        stateMachine.Add("liststay", new ListModalDialogStayState());
        stateMachine.Add("listhide", new ListModalDialogHideState());
        stateMachine.Add("confirmshow", new ConfirmModalDialogShowState());
        stateMachine.Add("confirmstay", new ConfirmModalDialogStayState());
        stateMachine.Add("commitshow", new CommitModalDialogShowState());
        stateMachine.Add("commitstay", new CommitModalDialogStayState());
        stateMachine.Change("listhide");
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
        if (notifyMessage == NotifyMessage.ShopShow)
            stateMachine.Change("listshow");
        else if (notifyMessage == NotifyMessage.ShopHide)
            stateMachine.Change("listhide");
        else if (notifyMessage == NotifyMessage.ShopCommitShow)
            stateMachine.Change("commitshow", parameter);
        else if (notifyMessage == NotifyMessage.ShopConfirmShow) stateMachine.Change("confirmshow", parameter);
    }

    public FiniteStateMachine<ShopCanvasBehaviour> stateMachine { get; set; }
}