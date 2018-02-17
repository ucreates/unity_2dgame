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
using Frontend.Component.Asset;
namespace Frontend.Component.Asset.Sound {
public abstract class BaseSoundAsset : BaseAsset {
    public override string type {
        get {
            return "sound";
        }
    }
    protected bool enablePlay {
        get;
        set;
    }
    protected bool onlyOncePlay {
        get;
        set;
    }
    public BaseSoundAsset() {
        this.onlyOncePlay = false;
        this.enablePlay = true;
    }
    public virtual void Play(bool loop) {
        return;
    }
    public virtual void Play() {
        return;
    }
    public virtual void Pause() {
        return;
    }
    public virtual void Stop() {
        return;
    }
    public void Reset() {
        this.enablePlay = true;
    }
}
}
