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
using UnityEngine;

namespace Frontend.Component.Asset.Renderer.Animator.Builder
{
    public abstract class BaseAnimatorAssetBuilder
    {
        protected float alpha { get; set; }

        public bool enabled { get; set; } = true;

        protected Vector3 position { get; set; }

        protected Vector3 scale { get; set; }

        protected Vector3 rotate { get; set; }

        protected List<GameObject> gameObjectList { get; set; } = new();

        protected List<Transform> transformList { get; set; } = new();

        public BaseAnimatorAssetBuilder AddAlpha(float alpha)
        {
            this.alpha = alpha;
            return this;
        }

        public BaseAnimatorAssetBuilder AddPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        public BaseAnimatorAssetBuilder AddScale(Vector3 scale)
        {
            this.scale = scale;
            return this;
        }

        public BaseAnimatorAssetBuilder AddRotate(Vector3 euler)
        {
            rotate = rotate;
            return this;
        }

        public BaseAnimatorAssetBuilder AddEnabled(bool enable)
        {
            enabled = enable;
            return this;
        }

        public BaseAnimatorAssetBuilder AddGameObject(GameObject gameObject)
        {
            gameObjectList.Add(gameObject);
            return this;
        }

        public BaseAnimatorAssetBuilder AddTransform(Transform transform)
        {
            transformList.Add(transform);
            return this;
        }

        public virtual void Build()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Reset()
        {
        }
    }
}