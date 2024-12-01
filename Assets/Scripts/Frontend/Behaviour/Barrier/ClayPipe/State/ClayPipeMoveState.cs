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
    public sealed class ClayPipeMoveState : FiniteState<ClayPipeBehaviour>
    {
        private const float MOVE_SPEED = 1.5f;

        private Vector3 destroyPosition { get; set; }

        public override void Create()
        {
            destroyPosition = new Vector3(-5f, 0f, 0f);
        }

        public override void Update()
        {
            if (owner.transform.position.x > destroyPosition.x)
                owner.transform.position -= new Vector3(MOVE_SPEED, 0f, 0f) * Time.deltaTime;
            else
                owner.stateMachine.Change("destroy");
        }
    }
}