//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.Asset.Renderer.Animator.Builder;
using Frontend.Component.State;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class ClayPipeStopState : FiniteState<ClayPipeBehaviour> {
    public override void Create() {
        Transform down = this.owner.transform.Find("ClayPipeDown");
        Transform up = this.owner.transform.Find("ClayPipeUp");
        Transform hitarea = this.owner.transform.Find("HitArea");
        CrayPipeAssetBuilder builder = new CrayPipeAssetBuilder();
        builder
        .AddTransform(down)
        .AddTransform(up)
        .AddTransform(hitarea)
        .Build();
    }
}
}
