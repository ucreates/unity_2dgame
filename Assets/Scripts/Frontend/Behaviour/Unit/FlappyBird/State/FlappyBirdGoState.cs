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
using System.Collections.Generic;
using Core.Validator;
using Core.Validator.Entity;
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
namespace Frontend.Behaviour.State {
public sealed class FlappyBirdGoState : FiniteState<FlappyBirdBehaviour> {
    private Rigidbody2D rigidBody {
        get;
        set;
    }
    public override void Create() {
        this.rigidBody = this.owner.GetComponent<Rigidbody2D>();
        this.rigidBody.gravityScale = 0.25f;
        this.rigidBody.velocity = Vector2.zero;
        this.rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
        SoundEffectAsset soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("bird_wing") as SoundEffectAsset;
        soundAsset.Play();
    }
    public override void Update() {
        if (Input.GetMouseButtonDown(0)) {
            this.rigidBody.velocity = Vector2.zero;
            this.rigidBody.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
        } else {
            this.owner.stateMachine.Change("fall", true);
        }
        BaseValidator validator = new ScreenValidator();
        ValidatorResponse res = validator.IsValid(this.owner.transform.position);
        List<bool> ret = res.GetResultList();
        if (false != ret[0]) {
            this.owner.stateMachine.Change("dead");
        }
    }
}
}
