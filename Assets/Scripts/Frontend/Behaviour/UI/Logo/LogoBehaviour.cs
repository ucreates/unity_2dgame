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
using Core.Entity;
using Core.Scene;
using Frontend.Component.Asset.Render;
using Frontend.Component.Property;
using Service;
public sealed class LogoBehaviour : BaseBehaviour {
    public void Start() {
        this.property = new BaseProperty(this);
        ServiceGateway
        .GetInstance()
        .Request("service://master/init")
        .Update();
        Spine.AnimationState.CompleteDelegate callback = (Spine.AnimationState state , int trackIndex , int loopCount) => {
            this.StartCoroutine(Director.Translate("game", 0.0f));
        };
        AnimatorAsset asset = new AnimatorAsset(this);
        asset.Play("show", callback, false);
    }
}
