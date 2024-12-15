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
using Core.Extensions;

namespace Frontend.Component.Asset.Sound
{
    public class SoundAssetCollection
    {
        private SoundAssetCollection()
        {
            soundBGMDictionary = new Dictionary<string, BaseSoundAsset>();
            soundEffectDictionary = new Dictionary<string, BaseSoundAsset>();
        }

        public Dictionary<string, BaseSoundAsset> soundBGMDictionary { get; set; }

        public Dictionary<string, BaseSoundAsset> soundEffectDictionary { get; set; }

        private static SoundAssetCollection instance { get; set; }

        public static SoundAssetCollection GetInstance()
        {
            if (null == instance) instance = new SoundAssetCollection();
            return instance;
        }

        public BaseSoundAsset GetBGMAsset(string bgmName)
        {
            if (false == soundBGMDictionary.ContainsKey(bgmName)) return null;
            return soundBGMDictionary[bgmName];
        }

        public BaseSoundAsset GetSEAsset(string seName)
        {
            if (false == soundEffectDictionary.ContainsKey(seName)) return null;
            return soundEffectDictionary[seName];
        }

        public bool SetBGMAsset(string bgmName, BaseSoundAsset bgmAsseet)
        {
            if (soundBGMDictionary.ContainsKey(bgmName)) return false;
            soundBGMDictionary.Add(bgmName, bgmAsseet);
            return true;
        }

        public bool SetSEAsset(string seName, BaseSoundAsset seAsseet)
        {
            if (soundBGMDictionary.ContainsKey(seName)) return false;
            soundEffectDictionary.Add(seName, seAsseet);
            return true;
        }

        public void Stop()
        {
            soundEffectDictionary.ForEach(pair =>
            {
                pair.Value.Stop();
                pair.Value.Reset();
            });

            soundBGMDictionary.ForEach(pair =>
            {
                pair.Value.Stop();
                pair.Value.Reset();
            });
        }

        public void Clear()
        {
            soundEffectDictionary.Clear();
            soundBGMDictionary.Clear();
        }
    }
}