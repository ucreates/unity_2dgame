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

namespace Frontend.Component.Asset.Render
{
    public sealed class MaterialAsset : BaseRenderAsset
    {
        public MaterialAsset(BaseBehaviour owner)
        {
            this.owner = owner;
            var renderer = owner.GetComponent<UnityEngine.Renderer>();
            if (null != renderer)
                material = renderer.material;
            else
                material = null;
        }

        private Material material { get; }

        public void Play(Vector2 uv)
        {
            if (null == material)
            {
                Debug.LogError("material is null");
                return;
            }

            material.mainTextureOffset = uv;
        }

        public void Play(float offset)
        {
            Play(new Vector2(offset, 0));
        }
    }
}