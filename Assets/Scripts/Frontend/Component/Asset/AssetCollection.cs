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

namespace Frontend.Component.Asset
{
    public sealed class AssetCollection
    {
        public AssetCollection()
        {
            assetDictionary = new Dictionary<string, BaseAsset>();
        }

        public Dictionary<string, BaseAsset> assetDictionary { get; set; }

        public BaseAsset Get(string assetName)
        {
            return assetDictionary.FirstOrDefault(pair => pair.Key == assetName).Value;
        }

        public bool Set(string assetName, BaseAsset asset)
        {
            if (assetDictionary.ContainsKey(assetName)) return false;
            assetDictionary.Add(assetName, asset);
            return true;
        }

        public void Clear()
        {
            assetDictionary.Clear();
        }
    }
}