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
using Frontend.Component.Asset.Render;
using Frontend.Component.Property;
using Service;
using Spine;

public sealed class LogoBehaviour : BaseBehaviour
{
    public void Start()
    {
        property = new BaseProperty(this);
        ServiceGateway
            .GetInstance()
            .Request("service://master/init")
            .Update();
        AnimationState.TrackEntryDelegate callback = state => { StartCoroutine(Director.Translate("game", 0.0f)); };
        var asset = new AnimatorAsset(this);
        asset.Play("show", callback, false);
    }
}