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
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class FlappyBIrdHideState : FiniteState<FlappyBirdBehaviour>
    {
        public override void Create()
        {
            owner.GetComponent<Renderer>().enabled = false;
            var rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0f;
        }

        public override void Update()
        {
            owner.transform.position = owner.defaultPosition;
        }
    }
}