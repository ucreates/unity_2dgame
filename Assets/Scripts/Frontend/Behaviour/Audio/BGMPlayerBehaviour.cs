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
using Frontend.Component.Asset.Sound;
using Frontend.Component.Property;
using Frontend.Notify;
public sealed class BGMPlayerBehaviour : BaseBehaviour, INotify {
    public void Start() {
        this.property = new BaseProperty(this);
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameStart) {
            BGMAsset bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("athletic") as BGMAsset;
            bgmAsset.Play(this.gameObject, true);
        } else if (notifyMessage == NotifyMessage.FlappyBirdDead) {
            BGMAsset bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("athletic") as BGMAsset;
            bgmAsset.Stop();
        } else if (notifyMessage == NotifyMessage.GameOver) {
            BGMAsset bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("player_down") as BGMAsset;
            bgmAsset.Play(this.gameObject);
        } else if (notifyMessage == NotifyMessage.GameRestart) {
            SoundAssetCollection.GetInstance().Stop();
        }
    }
}
