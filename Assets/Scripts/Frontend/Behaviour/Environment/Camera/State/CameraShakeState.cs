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
using Frontend.Component.Vfx.Easing;
using Frontend.Component.State;
namespace Frontend.Behaviour.State {
public sealed class CameraShakeState : FiniteState<CameraBehaviour> {
    private const float LIMIT_SHAKE_RATE = 0.05f;
    private Shake shake {
        get;
        set;
    }
    public override void Create() {
        this.shake = new Shake(CameraBehaviour.DEFAULT_OTHROGRAPHIC_SIZE);
    }
    public override void Update() {
        Camera.main.orthographicSize =  this.shake.Update();
        if (CameraShakeState.LIMIT_SHAKE_RATE > this.shake.rate) {
            Camera.main.orthographicSize = this.shake.baseValue;
            this.owner.stateMachine.Change("stop");
        }
    }
}
}
