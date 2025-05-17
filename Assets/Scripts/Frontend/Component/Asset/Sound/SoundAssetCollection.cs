//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using System.Linq;

namespace Frontend.Component.Asset.Sound
{
    public class SoundAssetCollection
    {
        private SoundAssetCollection()
        {
            soundList = new List<BaseSoundAsset>();
        }

        public List<BaseSoundAsset> soundList { get; set; }

        private static SoundAssetCollection instance { get; set; }

        public static SoundAssetCollection GetInstance()
        {
            instance ??= new SoundAssetCollection();
            return instance;
        }

        public BGMAsset GetBGMAsset(string bgmName)
        {
            return soundList.OfType<BGMAsset>().FirstOrDefault(bgm => bgm.name.Equals(bgmName));
        }

        public SoundEffectAsset GetSeAsset(string seName)
        {
            return soundList.OfType<SoundEffectAsset>().FirstOrDefault(se => se.name.Equals(seName));
        }

        public void SetBGMAsset(string bgmName, BaseSoundAsset bgmAsseet)
        {
            bgmAsseet.name = bgmName;
            soundList.Add(bgmAsseet);
        }

        public void SetSeAsset(string seName, BaseSoundAsset seAsseet)
        {
            seAsseet.name = seName;
            soundList.Add(seAsseet);
        }

        public void Stop()
        {
            soundList.ForEach(sound =>
            {
                sound.Stop();
                sound.Reset();
            });
        }

        public void Clear()
        {
            soundList.Clear();
        }
    }
}