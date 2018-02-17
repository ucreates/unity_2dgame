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
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.State;
using Frontend.Component.Asset.Render;
namespace Frontend.Behaviour.State {
public sealed class LandScrollState : FiniteState<LandBehaviour> {
    private MaterialAsset renderAsset {
        get;
        set;
    }
    public override void Create() {
        this.timeLine = new TimeLine();
        this.renderAsset = this.owner.assetCollection.Get("anime") as MaterialAsset;
    }
    public override void Update() {
        float offset = this.timeLine.currentTime * LandBehaviour.UV_SCROLL_RATE;
        this.renderAsset.Play(offset);
        this.timeLine.Next();
    }
}
}
