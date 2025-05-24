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
        public List<BaseAsset> assetList { get; } = new();

        public T Get<T>(string assetName) where T : BaseAsset
        {
            return assetList.OfType<T>().FirstOrDefault(asset => asset.name.Equals(assetName));
        }

        public void Set(string assetName, BaseAsset asset)
        {
            asset.name = assetName;
            assetList.Add(asset);
        }

        public void Clear()
        {
            assetList.Clear();
        }
    }
}