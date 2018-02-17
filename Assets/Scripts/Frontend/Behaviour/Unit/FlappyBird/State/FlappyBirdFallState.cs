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
using Service.Strategy;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.Vfx.Easing;
using Frontend.Component.State;
using Core.Entity;
namespace Frontend.Behaviour.State {
public  class FlappyBirdFallState : FiniteState<FlappyBirdBehaviour> {
    protected Rigidbody2D rigidBody {
        get;
        set;
    }
    public override void Create() {
        this.rigidBody = this.owner.GetComponent<Rigidbody2D>();
        this.rigidBody.gravityScale = 0.25f;
        this.timeLine = new TimeLine();
    }
    public override void Update() {
        if (Input.GetMouseButtonDown(0)) {
            this.rigidBody.velocity = Vector2.zero;
            this.rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
            this.owner.stateMachine.Change("go", true);
        } else {
            float falldown = Quadratic.EaseIn(this.timeLine.currentTime, 0.0f, 90.0f, 1.5f);
            Vector3 nextEuler = new Vector3(0f, 0f, falldown) * -1f;
            this.owner.transform.rotation = Quaternion.Euler(nextEuler);
            this.timeLine.Next(1f);
        }
    }
}
}
