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
using System.Collections.Generic;
namespace Frontend.Component.Asset.Sound {
public sealed class BGMAsset : BaseSoundAsset {
    private AudioSource audioSource {
        get;
        set;
    }
    private AudioClip audioClip {
        get;
        set;
    }
    public BGMAsset(string assetName, bool onlyOncePlay = false) {
        this.audioClip = Resources.Load(assetName) as AudioClip;
        this.audioSource = null;
        this.onlyOncePlay = onlyOncePlay;
    }
    public void Play(GameObject player, bool loop) {
        if (false == this.enablePlay) {
            return;
        }
        if (false != this.onlyOncePlay && false != this.enablePlay) {
            this.enablePlay = false;
        }
        this.audioSource = player.GetComponent<AudioSource>();
        this.audioSource.clip = this.audioClip;
        this.audioSource.volume = 1.0f;
        this.audioSource.loop = loop;
        this.audioSource.Play();
        return;
    }
    public void Play(string playerName, bool loop) {
        GameObject player =  GameObject.Find(playerName);
        if (null == player) {
            Debug.LogError("not exist audio player::" + playerName);
            return;
        }
        this.Play(player, loop);
        return;
    }
    public void Play(GameObject player) {
        this.Play(player, false);
    }
    public void Play(string playerName) {
        this.Play(playerName, false);
    }
    public override void Pause() {
        if (null == this.audioSource) {
            return;
        }
        this.audioSource.Pause();
    }
    public override void Stop() {
        if (null == this.audioSource) {
            return;
        }
        this.audioSource.Stop();
    }
}
}
