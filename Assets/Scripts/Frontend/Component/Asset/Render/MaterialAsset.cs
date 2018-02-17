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
using Spine.Unity;
namespace Frontend.Component.Asset.Render {
public sealed class MaterialAsset : BaseRenderAsset {
    private Material material {
        get;
        set;
    }
    public MaterialAsset(BaseBehaviour owner) {
        this.owner = owner;
        UnityEngine.Renderer renderer = owner.GetComponent<UnityEngine.Renderer>();
        if (null != renderer) {
            this.material = renderer.material;
        } else {
            this.material = null;
        }
    }
    public void Play(Vector2 uv) {
        if (null == this.material) {
            Debug.LogError("material is null");
            return;
        }
        this.material.mainTextureOffset = uv;
    }
    public void Play(float offset) {
        this.Play(new Vector2(offset, 0));
    }
}
}
