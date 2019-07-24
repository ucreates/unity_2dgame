//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Scene;
using Frontend.Behaviour.Base;
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
        Spine.AnimationState.TrackEntryDelegate callback = (Spine.TrackEntry state) => {
            Director direcor = Director.GetInstance();
            direcor.Translate("game");
            return;
        };
        AnimatorAsset asset = new AnimatorAsset(this);
        asset.Play("show", callback, false);
        return;
    }
}
