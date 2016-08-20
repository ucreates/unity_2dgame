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
namespace Core.Validator.Config {
public sealed class UnityConfigAssetLoader : BaseConfigAssetLoader {
    public override string Load(string assetName) {
        UnityEngine.Object text = Resources.Load(assetName) as UnityEngine.Object;
        if (null == text) {
            return string.Empty;
        }
        TextAsset xml = UnityEngine.Object.Instantiate(text) as TextAsset;
        return xml.text;
    }
}
}
