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
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;
namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class CommitModalDialogShowState : FiniteState<ShopCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private ShopCanvasListModalDialogBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    private Parameter notifyParameter {
        get;
        set;
    }
    public override void Create(Parameter paramter) {
        this.notifyParameter = paramter;
        string message = paramter.Get<string>("message");
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.alphaTimeLine = new TimeLine();
        this.previousAlpha = 0f;
        foreach (Transform child in this.owner.transform) {
            if (child.name.Equals("CommitModalDialog")) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
        if (null == this.builder) {
            this.builder = new ShopCanvasListModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        this.builder
        .AddCommitMessage(message)
        .AddTransform(this.owner.transform)
        .AddAlpha(0f)
        .AddEnabled(false)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("commitstay", this.notifyParameter);
            return;
        }
        this.builder
        .AddAlpha(alpha)
        .Update();
        this.previousAlpha = alpha;
        this.alphaTimeLine.Next(1.5f);
    }
}
}
