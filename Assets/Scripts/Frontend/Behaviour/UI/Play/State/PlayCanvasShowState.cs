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
public sealed class PlayCanvasShowState : FiniteState<PlayCanvasBehaviour> {
    private BaseStrategy strategy {
        get;
        set;
    }
    private PlayCanvasBuilder builder {
        get;
        set;
    }
    public override void Create() {
        this.strategy = ServiceGateway.GetInstance().Request("service://stats/player");
        this.strategy.Clear();
        Response ret = this.strategy.Get();
        string nickName = ret.Get<string>("nickname");
        string copyright = ret.Get<string>("copyright");
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        if (null == this.builder) {
            this.builder = new PlayCanvasBuilder();
        } else {
            this.builder.Reset();
        }
        this.builder
        .AddNickName(nickName)
        .AddCopyright(copyright)
        .AddSprite(this.owner.scoreSpriteList)
        .AddTransform(this.owner.transform)
        .AddPosition(new Vector3(0f, 250f, 0f))
        .Build();
    }
    public override void Update() {
        Response ret = this.strategy.Get();
        int clearCount = ret.Get<int>("clearcount");
        int figure = Figure.CountFigure(clearCount);
        this.builder
        .AddClearCount(clearCount)
        .AddFigure(figure)
        .Update();
    }
}
}
