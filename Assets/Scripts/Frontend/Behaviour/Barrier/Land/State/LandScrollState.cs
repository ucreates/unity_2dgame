//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.Asset.Render;
using Frontend.Component.State;
using Frontend.Component.Vfx;

namespace Frontend.Behaviour.State
{
    public sealed class LandScrollState : FiniteState<LandBehaviour>
    {
        private MaterialAsset renderAsset { get; set; }

        public override void Create()
        {
            timeLine = new TimeLine();
            renderAsset = owner.assetCollection.Get<MaterialAsset>("anime");
        }

        public override void Update()
        {
            var offset = timeLine.currentTime * LandBehaviour.UV_SCROLL_RATE;
            renderAsset.Play(offset);
            timeLine.Next();
        }
    }
}