﻿//======================================================================
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
using Frontend.Component.Vfx.Sprine;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public class FlappyBirdReadyState : FiniteState<FlappyBirdBehaviour>
    {
        private TimeLine sprineTimeLine { get; } = new();

        public override void Create()
        {
            var animator = owner?.assetCollection?.Get<AnimatorAsset>("anime");
            var renderer = owner.GetComponent<Renderer>();
            animator?.Play("fly");
            renderer.enabled = true;
            owner.deadTimeLine = new TimeLine();
            owner.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            var rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0f;
        }

        public override void Update()
        {
            var time = sprineTimeLine?.currentTime ?? 0f;
            var y = Periodic.Sin(time, 0.35f);
            owner.transform.position = owner.defaultPosition + new Vector2(0f, y);
            owner.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            sprineTimeLine?.Next(3f);
        }
    }
}