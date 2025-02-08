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
using UnityPlugin;
using UnityPlugin.Frontend.View;

public sealed class NoticeCanvasBehaviour : BaseBehaviour, IStateMachine<NoticeCanvasBehaviour>, INotify
{
    public WebViewPlugin webViewPlugin { get; set; }

    public void Start()
    {
        rx = Notifier.GetInstance().OnNotify().Where(message => { return message.title == NotifyMessage.Title.NoticeShow || message.title == NotifyMessage.Title.NoticeHide; }).Subscribe(message => { OnNotify(message); });
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<NoticeCanvasBehaviour>(this);
        stateMachine.Add("show", new NoticeCanvasShowState());
        stateMachine.Add("stay", new NoticeCanvasStayState());
        stateMachine.Add("hide", new NoticeCanvasHideState());
        stateMachine.Change("hide");
        stateMachine.Play();
        webViewPlugin = PluginFactory.GetPlugin<WebViewPlugin>();
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage.title == NotifyMessage.Title.NoticeShow)
            stateMachine.Change("show");
        else if (notifyMessage.title == NotifyMessage.Title.NoticeHide)
            stateMachine.Change("hide");
    }

    public FiniteStateMachine<NoticeCanvasBehaviour> stateMachine { get; set; }
}