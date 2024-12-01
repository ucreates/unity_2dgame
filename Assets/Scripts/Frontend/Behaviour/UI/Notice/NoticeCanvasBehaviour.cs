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
using UnityPlugin;
using UnityPlugin.Frontend.View;

public sealed class NoticeCanvasBehaviour : BaseBehaviour, IStateMachine<NoticeCanvasBehaviour>, INotify
{
    public WebViewPlugin webViewPlugin { get; set; }

    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<NoticeCanvasBehaviour>(this);
        stateMachine.Add("show", new NoticeCanvasShowState());
        stateMachine.Add("stay", new NoticeCanvasStayState());
        stateMachine.Add("hide", new NoticeCanvasHideState());
        stateMachine.Change("hide");
        stateMachine.Play();
        webViewPlugin = PluginFactory.GetPlugin<WebViewPlugin>();
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.NoticeShow)
            stateMachine.Change("show");
        else if (notifyMessage == NotifyMessage.NoticeHide) stateMachine.Change("hide");
    }

    public FiniteStateMachine<NoticeCanvasBehaviour> stateMachine { get; set; }
}