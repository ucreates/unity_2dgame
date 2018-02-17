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
public sealed class ClayPipeMoveState : FiniteState<ClayPipeBehaviour> {
    private const float MOVE_SPEED = 1.5f;
    private Vector3 destroyPosition {
        get;
        set;
    }
    public override void Create() {
        this.destroyPosition = new Vector3(-5f, 0f, 0f);
    }
    public override void Update() {
        if (this.owner.transform.position.x > this.destroyPosition.x) {
            this.owner.transform.position -= new Vector3(ClayPipeMoveState.MOVE_SPEED, 0f, 0f) * Time.deltaTime;
        } else {
            this.owner.stateMachine.Change("destroy");
        }
    }
}
}
