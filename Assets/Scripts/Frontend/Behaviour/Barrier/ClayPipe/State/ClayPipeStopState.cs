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

namespace Frontend.Behaviour.State
{
    public sealed class ClayPipeStopState : FiniteState<ClayPipeBehaviour>
    {
        public override void Create()
        {
            var downObject = owner.transform.Find("ClayPipeDown");
            var upObject = owner.transform.Find("ClayPipeUp");
            var hitAreaObject = owner.transform.Find("HitArea");
            var builder = new CrayPipeAssetBuilder();
            builder
                ?.AddTransform(downObject)
                ?.AddTransform(upObject)
                ?.AddTransform(hitAreaObject)
                ?.Build();
        }
    }
}