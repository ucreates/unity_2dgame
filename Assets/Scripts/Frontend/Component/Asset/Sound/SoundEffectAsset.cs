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
using System;
using System.Collections;
using System.Collections.Generic;
namespace Frontend.Component.Asset.Sound {
public sealed class SoundEffectAsset : BaseSoundAsset {
    private AudioClip audioClip {
        get;
        set;
    }
    public SoundEffectAsset(string assetName, bool onlyOncePlay = false) {
        this.audioClip = Resources.Load(assetName) as AudioClip;
        this.onlyOncePlay = onlyOncePlay;
    }
    public override void Play() {
        if (false == this.enablePlay || null == this.audioClip) {
            return;
        }
        if (false != this.onlyOncePlay && false != this.enablePlay) {
            this.enablePlay = false;
        }
        AudioSource.PlayClipAtPoint(this.audioClip, Vector3.zero, 1.0f);
    }
    public IEnumerator Delay(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        this.Play();
    }
}
}
