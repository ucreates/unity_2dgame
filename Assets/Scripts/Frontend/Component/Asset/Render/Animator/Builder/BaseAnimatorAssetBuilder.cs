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
using System.Collections.Generic;
namespace Frontend.Component.Asset.Renderer.Animator.Builder {
public abstract class BaseAnimatorAssetBuilder {
    protected float alpha {
        get;
        set;
    }
    public bool enabled {
        get;
        set;
    }
    protected Vector3 position {
        get;
        set;
    }
    protected Vector3 scale {
        get;
        set;
    }
    protected Vector3 rotate {
        get;
        set;
    }
    protected List<GameObject> gameObjectList {
        get;
        set;
    }
    protected List<Transform> transformList {
        get;
        set;
    }
    public BaseAnimatorAssetBuilder() {
        this.alpha = 0f;
        this.enabled = true;
        this.gameObjectList = new List<GameObject>();
        this.transformList = new List<Transform>();
    }
    public BaseAnimatorAssetBuilder AddAlpha(float alpha) {
        this.alpha = alpha;
        return this;
    }
    public BaseAnimatorAssetBuilder AddPosition(Vector3 position) {
        this.position = position;
        return this;
    }
    public BaseAnimatorAssetBuilder AddScale(Vector3 scale) {
        this.scale = scale;
        return this;
    }
    public BaseAnimatorAssetBuilder AddRotate(Vector3 euler) {
        this.rotate = rotate;
        return this;
    }
    public BaseAnimatorAssetBuilder AddEnabled(bool enable) {
        this.enabled = enable;
        return this;
    }
    public BaseAnimatorAssetBuilder AddGameObject(GameObject gameObject) {
        this.gameObjectList.Add(gameObject);
        return this;
    }
    public BaseAnimatorAssetBuilder AddTransform(Transform transform) {
        this.transformList.Add(transform);
        return this;
    }
    public virtual void Build() {
        return;
    }
    public virtual void Update() {
        return;
    }
    public virtual void Reset() {
        return;
    }
}
}
