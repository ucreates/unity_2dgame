//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.Math;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Service;
using Service.Strategy;
namespace Frontend.Behaviour.State {
public sealed class NoticeCanvasStayState : FiniteState<NoticeCanvasBehaviour> {
    private NoticeCanvasBuilder builder {
        get;
        set;
    }
    public override void Create() {
        if (null == this.builder) {
            this.builder = new NoticeCanvasBuilder();
        } else {
            this.builder.Reset();
        }
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.builder = new NoticeCanvasBuilder();
        this.builder
        .AddTransform(this.owner.transform)
        .AddAlpha(1f)
        .AddEnabled(true)
        .Update();
    }
}
}
