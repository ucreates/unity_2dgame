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
namespace Frontend.Component.Asset {
public sealed class AssetCollection {
    public Dictionary<string, BaseAsset> assetDictionary {
        get;
        set;
    }
    public AssetCollection() {
        this.assetDictionary = new Dictionary<string, BaseAsset>();
    }
    public BaseAsset Get(string assetName) {
        if (false == this.assetDictionary.ContainsKey(assetName)) {
            return null;
        }
        return this.assetDictionary[assetName];
    }
    public bool Set(string assetName, BaseAsset asset) {
        if (false != this.assetDictionary.ContainsKey(assetName)) {
            return false;
        }
        this.assetDictionary.Add(assetName, asset);
        return true;
    }
    public void Clear() {
        this.assetDictionary.Clear();
        return;
    }
}
}
