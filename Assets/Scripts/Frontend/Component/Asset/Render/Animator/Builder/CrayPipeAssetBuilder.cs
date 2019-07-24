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
namespace Frontend.Component.Asset.Renderer.Animator.Builder
{
    public sealed class CrayPipeAssetBuilder : BaseAnimatorAssetBuilder {
    public override void Build() {
        foreach (Transform transform in this.transformList) {
            BoxCollider2D boxCollider = transform.GetComponent<BoxCollider2D>();
            if (null != boxCollider) {
                boxCollider.enabled = false;
                boxCollider.isTrigger = true;
            }
        }
    }
}
}
