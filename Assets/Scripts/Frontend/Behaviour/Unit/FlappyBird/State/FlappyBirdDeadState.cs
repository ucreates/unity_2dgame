//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Easing;
using Frontend.Notify;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class FlappyBirdDeadState : FiniteState<FlappyBirdBehaviour> {
    private Rigidbody2D rigidBody {
        get;
        set;
    }
    private Vector3 currentEuler {
        get;
        set;
    }
    private Vector3 deadPosition {
        get;
        set;
    }
    public override void Create() {
        this.rigidBody = this.owner.GetComponent<Rigidbody2D>();
        this.rigidBody.gravityScale = 0.5f;
        this.deadPosition = this.owner.transform.position;
        this.timeLine = new TimeLine();
        Notifier notifier = Notifier.GetInstance();
        if (notifier.currentMessage != NotifyMessage.FlappyBirdDead) {
            notifier.Notify(NotifyMessage.FlappyBirdDead);
        }
        Core.Object.Resource.Instanciate("Prefabs/Curtain", new UnityEngine.Vector3(0f, 0f, 0f), UnityEngine.Quaternion.identity);
        SoundEffectAsset soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("bird_die") as SoundEffectAsset;
        this.owner.StartCoroutine(soundAsset.Delay(1.0f));
    }
    public override void Update() {
        float falldown = Quadratic.EaseIn(this.timeLine.currentTime, 0.0f, 90.0f, 1.5f);
        Vector3 nextEuler = new Vector3(0f, 0f, falldown) * -1f;
        this.owner.transform.rotation = Quaternion.Euler(nextEuler);
        this.timeLine.Next(1f);
    }
}
}
