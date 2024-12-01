//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Generator;
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Easing;
using Frontend.Notify;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class FlappyBirdDeadState : FiniteState<FlappyBirdBehaviour>
    {
        private Rigidbody2D rigidBody { get; set; }

        private Vector3 currentEuler { get; set; }

        private Vector3 deadPosition { get; set; }

        public override void Create()
        {
            rigidBody = owner.GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0.5f;
            deadPosition = owner.transform.position;
            timeLine = new TimeLine();
            var notifier = Notifier.GetInstance();
            if (notifier.currentMessage != NotifyMessage.FlappyBirdDead) notifier.Notify(NotifyMessage.FlappyBirdDead);
            ResourceGenerator.Generate("Prefabs/Curtain", new Vector3(0f, 0f, 0f), Quaternion.identity);
            var soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("bird_die") as SoundEffectAsset;
            owner.StartCoroutine(soundAsset.Delay(1.0f));
        }

        public override void Update()
        {
            var falldown = Quadratic.EaseIn(timeLine.currentTime, 0.0f, 90.0f, 1.5f);
            var nextEuler = new Vector3(0f, 0f, falldown) * -1f;
            owner.transform.rotation = Quaternion.Euler(nextEuler);
            timeLine.Next();
        }
    }
}