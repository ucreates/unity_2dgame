//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Frontend.Component.Asset.Sound;
using Frontend.Component.Property;
using Frontend.Notify;

public sealed class BGMPlayerBehaviour : BaseBehaviour, INotify
{
    public void Start()
    {
        property = new BaseProperty(this);
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.GameStart)
        {
            var bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("athletic") as BGMAsset;
            bgmAsset.Play(gameObject, true);
        }
        else if (notifyMessage == NotifyMessage.FlappyBirdDead)
        {
            var bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("athletic") as BGMAsset;
            bgmAsset.Stop();
        }
        else if (notifyMessage == NotifyMessage.GameOver)
        {
            var bgmAsset = SoundAssetCollection.GetInstance().GetBGMAsset("player_down") as BGMAsset;
            bgmAsset.Play(gameObject);
        }
        else if (notifyMessage == NotifyMessage.GameRestart)
        {
            SoundAssetCollection.GetInstance().Stop();
        }
    }
}