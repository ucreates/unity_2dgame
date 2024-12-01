//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class CameraShakeState : FiniteState<CameraBehaviour>
    {
        private const float LIMIT_SHAKE_RATE = 0.05f;

        private Shake shake { get; set; }

        public override void Create()
        {
            shake = new Shake(CameraBehaviour.DEFAULT_OTHROGRAPHIC_SIZE);
        }

        public override void Update()
        {
            Camera.main.orthographicSize = shake.Update();
            if (LIMIT_SHAKE_RATE > shake.rate)
            {
                Camera.main.orthographicSize = shake.baseValue;
                owner.stateMachine.Change("stop");
            }
        }
    }
}