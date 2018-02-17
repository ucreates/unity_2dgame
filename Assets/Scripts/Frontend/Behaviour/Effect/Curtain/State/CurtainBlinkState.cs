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
using System.Collections;
using Service;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.Vfx.Easing;
using Frontend.Component.State;
namespace Frontend.Behaviour.State {
public sealed class CurtainBlinkState : FiniteState<CurtainBehaviour> {
    private const float FRAME_RATE = 0.25f;
    private const float LIMIT_FRAME = 3.5f;
    private TimeLine blinkTimeLine {
        get;
        set;
    }
    private SpriteRenderer renderer {
        get;
        set;
    }
    public override void Create() {
        this.blinkTimeLine = new TimeLine();
        this.blinkTimeLine.rate = CurtainBlinkState.FRAME_RATE;
        this.renderer = this.owner.GetComponent<SpriteRenderer>();
        var color = this.renderer.color;
        color.a = 0.1f;
        this.renderer.color = color;
    }
    public override void Update() {
        float frame = this.blinkTimeLine.currentFrame;
        float alpha = Flash.Update(frame, 1.0f, 0.8f);
        if (CurtainBlinkState.LIMIT_FRAME <= frame) {
            alpha = 0f;
        }
        var color = this.renderer.color;
        color.a = alpha;
        this.renderer.color = color;
        this.blinkTimeLine.Next();
    }
}
}
