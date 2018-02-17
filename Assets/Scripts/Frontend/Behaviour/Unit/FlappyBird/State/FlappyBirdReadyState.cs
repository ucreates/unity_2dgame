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
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.State;
using Frontend.Component.Asset.Render;
namespace Frontend.Behaviour.State {
public  class FlappyBirdReadyState : FiniteState<FlappyBirdBehaviour> {
    private TimeLine sprineTimeLine {
        get;
        set;
    }
    public override void Create() {
        AnimatorAsset asset = this.owner.assetCollection.Get("anime") as AnimatorAsset;
        asset.Play("fly");
        this.owner.GetComponent<Renderer>().enabled = true;
        this.owner.deadTimeLine = new TimeLine();
        this.owner.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        Rigidbody2D rigidBody = this.owner.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0f;
        this.sprineTimeLine = new TimeLine();
    }
    public override void Update() {
        float currentTime = this.sprineTimeLine.currentTime;
        float vy = Periodic.Sin(currentTime, 0.35f);
        this.owner.transform.position = this.owner.defaultPosition + new Vector2(0f, vy);
        this.owner.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        this.sprineTimeLine.Next(3f);
    }
}
}
