//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Extensions;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class CurtainBlinkState : FiniteState<CurtainBehaviour>
    {
        private const float FRAME_RATE = 0.25f;
        private const float LIMIT_FRAME = 3.5f;

        private TimeLine blinkTimeLine { get; } = new()
        {
            rate = FRAME_RATE
        };

        private SpriteRenderer renderer { get; set; }

        public override void Create()
        {
            renderer = owner.GetComponent<SpriteRenderer>();
            renderer?.FillAlpha(0.1f);
        }

        public override void Update()
        {
            var frame = blinkTimeLine?.currentFrame ?? 0f;
            var alpha = Flash.Update(frame, 1.0f, 0.8f);
            if (LIMIT_FRAME <= frame) alpha = 0f;
            renderer?.FillAlpha(alpha);
            blinkTimeLine?.Next();
        }
    }
}