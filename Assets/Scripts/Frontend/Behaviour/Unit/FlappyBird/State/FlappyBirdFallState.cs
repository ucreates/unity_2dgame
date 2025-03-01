//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using Core.Extensions;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public class FlappyBirdFallState : FiniteState<FlappyBirdBehaviour>
    {
        protected Rigidbody2D rigidBody { get; set; }
        private Func<float, float, float, Vector3> cb => delegate(float x, float y, float z) { return new Vector3(x, y, z) * -1f; };

        public override void Create()
        {
            rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.25f;
            timeLine = new TimeLine();
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigidBody.linearVelocity = Vector2.zero;
                rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                owner?.stateMachine?.Change("go", true);
            }
            else
            {
                owner.transform.Easing(EaseType.QuadraticIn, Afin.Rotation, timeLine?.currentTime ?? 0f, 0.0f, 90.0f, 1.5f, false, false, true, cb);
                timeLine?.Next();
            }
        }
    }
}