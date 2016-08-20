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
using Frontend.Component.Asset.Sound;
namespace Frontend.Component.Asset.Sound {
public class SoundAssetCollection {
    public Dictionary<string, BaseSoundAsset> soundBGMDictionary {
        get;
        set;
    }
    public Dictionary<string, BaseSoundAsset> soundEffectDictionary {
        get;
        set;
    }
    private static SoundAssetCollection instance {
        get;
        set;
    }
    private SoundAssetCollection() {
        this.soundBGMDictionary = new Dictionary<string, BaseSoundAsset>();
        this.soundEffectDictionary = new Dictionary<string, BaseSoundAsset>();
    }
    public static SoundAssetCollection GetInstance() {
        if (null == SoundAssetCollection.instance) {
            SoundAssetCollection.instance = new SoundAssetCollection();
        }
        return SoundAssetCollection.instance;
    }
    public BaseSoundAsset GetBGMAsset(string bgmName) {
        if (false == this.soundBGMDictionary.ContainsKey(bgmName)) {
            return null;
        }
        return this.soundBGMDictionary[bgmName];
    }
    public BaseSoundAsset GetSEAsset(string seName) {
        if (false == this.soundEffectDictionary.ContainsKey(seName)) {
            return null;
        }
        return this.soundEffectDictionary[seName];
    }
    public bool SetBGMAsset(string bgmName, BaseSoundAsset bgmAsseet) {
        if (false != this.soundBGMDictionary.ContainsKey(bgmName)) {
            return false;
        }
        this.soundBGMDictionary.Add(bgmName, bgmAsseet);
        return true;
    }
    public bool SetSEAsset(string seName, BaseSoundAsset seAsseet) {
        if (false != this.soundBGMDictionary.ContainsKey(seName)) {
            return false;
        }
        this.soundEffectDictionary.Add(seName, seAsseet);
        return true;
    }
    public void Stop() {
        foreach (string key in this.soundEffectDictionary.Keys) {
            BaseSoundAsset asset = this.soundEffectDictionary[key];
            asset.Stop();
            asset.Reset();
        }
        foreach (string key in this.soundBGMDictionary.Keys) {
            BaseSoundAsset asset = this.soundBGMDictionary[key];
            asset.Stop();
            asset.Reset();
        }
    }
    public void Clear() {
        this.soundEffectDictionary.Clear();
        this.soundBGMDictionary.Clear();
    }
}
}