//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;
using UnityEngine.UI;
namespace Frontend.Behaviour.State
{
    public sealed class InputCanvasShowState : FiniteState<InputCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private InputCanvasModalDialogBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    public override void Create() {
        this.previousAlpha = 0f;
        this.alphaTimeLine = new TimeLine();
        if (null == this.builder) {
            this.builder = new InputCanvasModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        Transform md = this.owner.transform.Find("ModalDialog");
        if (null != md) {
            Image mdimg1 = md.GetComponent<Image>();
            mdimg1.gameObject.SetActive(true);
        }
        Transform ed = this.owner.transform.Find("ErrorDialog");
        if (null != ed) {
            Image edimg = ed.GetComponent<Image>();
            edimg.gameObject.SetActive(false);
        }
        Transform mdtr = this.owner.transform.Find("ModalDialog");
        Image mdimg2 = mdtr.GetComponent<Image>();
        this.builder
        .AddAlpha(0f)
        .AddTransform(mdimg2.transform)
        .AddEnabled(false)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("stay");
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
