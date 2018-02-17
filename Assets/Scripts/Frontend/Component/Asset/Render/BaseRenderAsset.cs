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
using Frontend.Component.Asset;
namespace Frontend.Component.Asset.Render {
public class BaseRenderAsset : BaseAsset {
    public BaseBehaviour owner {
        get;
        protected set;
    }
    public BaseRenderAsset() {
    }
    public BaseRenderAsset(BaseBehaviour owner) {
        this.owner = owner;
    }
    public virtual void Show() {
    }
    public virtual void Hide() {
    }
}
}
