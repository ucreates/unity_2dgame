//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityPlugin;
using UnityPlugin.Frontend.View;
using UnityEngine;
using System.Collections;
using Frontend.Notify;
using Frontend.Component.State;
using Frontend.Component.Property;
using Frontend.Behaviour.State;
using Core.Entity;
public sealed class NoticeCanvasBehaviour  : BaseBehaviour, IStateMachine<NoticeCanvasBehaviour>, INotify {
    public FiniteStateMachine<NoticeCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public WebViewPlugin webViewPlugin {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<NoticeCanvasBehaviour>(this);
        this.stateMachine.Add("show", new NoticeCanvasShowState());
        this.stateMachine.Add("stay", new NoticeCanvasStayState());
        this.stateMachine.Add("hide", new NoticeCanvasHideState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        this.webViewPlugin = PluginFactory.GetPlugin<WebViewPlugin>();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.NoticeShow) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.NoticeHide) {
            this.stateMachine.Change("hide");
        }
    }
}
