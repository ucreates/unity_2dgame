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
using Frontend.Behaviour.Base;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
using UnityEngine;
using UnityEngine.UI;
using UnityPlugin;
using UnityPlugin.Frontend.View;
public sealed class NoticeCanvasBehaviour  : BaseBehaviour, IStateMachine<NoticeCanvasBehaviour>, INotify {
    public Image webViewAreaImage;
    public FiniteStateMachine<NoticeCanvasBehaviour> stateMachine {
        get;
        set;
    }
    public WebViewPlugin webViewPlugin {
        get;
        set;
    }
    public Rect screenRect {
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
        this.screenRect = new Rect();
        this.webViewPlugin = PluginFactory.GetPlugin<WebViewPlugin>();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void Update() {
        this.stateMachine.Update();
    }
    private void OnGUI() {
        this.webViewPlugin.DrawWebViewAreaGizmo(this.screenRect);
        return;
    }
    public void OnNotify(int notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.NoticeShow) {
            this.stateMachine.Change("show");
        } else if (notifyMessage == NotifyMessage.NoticeHide) {
            this.stateMachine.Change("hide");
        }
    }
}
