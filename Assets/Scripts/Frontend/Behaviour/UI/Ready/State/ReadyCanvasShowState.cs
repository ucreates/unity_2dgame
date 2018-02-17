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
using Frontend.Notify;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.State;
namespace Frontend.Behaviour.State {
public sealed class ReadyCanvasShowState : FiniteState<ReadyCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private Image getReadyImage {
        get;
        set;
    }
    private Image tapLogoImage {
        get;
        set;
    }
    public override void Create() {
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.getReadyImage = GameObject.Find("GetReadyImage").GetComponent<Image>();
        this.tapLogoImage = GameObject.Find("TapScreenLogoImage").GetComponent<Image>();
        this.alphaTimeLine = new TimeLine();
    }
    public override void Update() {
        if (!Input.GetMouseButtonDown(0)) {
            float alpha = Flash.Update(this.alphaTimeLine.currentTime, 2f);
            float scale = Flash.Update(this.alphaTimeLine.currentTime, 7.0f, 0.1f);
            this.getReadyImage.color =  new Color(1.0f, 1.0f, 1.0f, alpha);
            this.tapLogoImage.rectTransform.localScale = new Vector2(1f + scale, 1f + scale);
            this.alphaTimeLine.Next();
        } else {
            this.owner.stateMachine.Change("hide");
            Notifier notifier = Notifier.GetInstance();
            if (notifier.currentMessage != NotifyMessage.GameStart) {
                notifier.Notify(NotifyMessage.GameStart);
            }
        }
    }
}
}
