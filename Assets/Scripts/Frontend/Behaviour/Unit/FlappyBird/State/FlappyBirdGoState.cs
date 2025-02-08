//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Linq;
using Core.Validator;
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class FlappyBirdGoState : FiniteState<FlappyBirdBehaviour>
    {
        private Rigidbody2D rigidBody { get; set; }

        public override void Create()
        {
            rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.25f;
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
            var soundAsset = SoundAssetCollection.GetInstance().GetSeAsset("bird_wing");
            soundAsset.Play();
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigidBody.linearVelocity = Vector2.zero;
                rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
            }
            else
            {
                owner.stateMachine.Change("fall", true);
            }

            BaseValidator validator = new ScreenValidator();
            var res = validator.IsValid(owner.transform.position);
            var ret = res.GetResultList();
            if (ret.FirstOrDefault()) owner.stateMachine.Change("dead");
        }
    }
}