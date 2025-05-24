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
using Core.Generator;
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Notify;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class FlappyBirdDeadState : FiniteState<FlappyBirdBehaviour>
    {
        private Rigidbody2D rigidBody { get; set; }

        private Vector3 deadPosition { get; set; }

        private Func<float, float, float, Vector3> cb => delegate(float x, float y, float z) { return new Vector3(x, y, z) * -1f; };

        public override void Create()
        {
            rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.5f;
            deadPosition = owner.transform.position;
            timeLine = new TimeLine();
            var notifier = Notifier.GetInstance();
            if (notifier?.currentMessage?.title != NotifyMessage.Title.FlappyBirdDead) notifier?.Notify(NotifyMessage.Title.FlappyBirdDead);
            ResourceGenerator.Generate("Prefabs/Curtain", new Vector3(0f, 0f, 0f), Quaternion.identity);
            var se = SoundAssetCollection.GetInstance().GetSeAsset("bird_die");
            owner.StartCoroutine(se?.Delay(1.0f));
        }

        public override void Update()
        {
            owner.transform.Easing(EaseType.QuadraticIn, Afin.Rotation, timeLine?.currentTime ?? 0f, 0.0f, 90.0f, 1.5f, false, false, true, cb);
            timeLine?.Next();
        }
    }
}