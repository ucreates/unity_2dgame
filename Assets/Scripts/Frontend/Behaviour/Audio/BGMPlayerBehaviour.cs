//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.Asset.Sound;
using Frontend.Notify;
using UniRx;

public sealed class BGMPlayerBehaviour : BaseBehaviour, INotify
{
    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.GameStart || message.title == NotifyMessage.Title.FlappyBirdDead || message.title == NotifyMessage.Title.GameOver || message.title == NotifyMessage.Title.GameRestart; })?.Subscribe(message => { OnNotify(message); });
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.GameStart)
        {
            var bgmAsset = SoundAssetCollection.GetInstance()?.GetBGMAsset("athletic");
            bgmAsset?.Play(gameObject, true);
        }
        else if (notifyMessage?.title == NotifyMessage.Title.FlappyBirdDead)
        {
            var bgmAsset = SoundAssetCollection.GetInstance()?.GetBGMAsset("athletic");
            bgmAsset?.Stop();
        }
        else if (notifyMessage?.title == NotifyMessage.Title.GameOver)
        {
            var bgmAsset = SoundAssetCollection.GetInstance()?.GetBGMAsset("player_down");
            bgmAsset?.Play(gameObject);
        }
        else if (notifyMessage?.title == NotifyMessage.Title.GameRestart)
        {
            SoundAssetCollection.GetInstance().Stop();
        }
    }
}