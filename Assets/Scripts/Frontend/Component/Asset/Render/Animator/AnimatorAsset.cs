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
public sealed class AnimatorAsset : BaseRenderAsset {
    public SkeletonAnimation anime {
        get;
        private set;
    }
    public AnimatorAsset() {
    }
    public AnimatorAsset(BaseBehaviour owner) {
        this.owner = owner;
        this.anime = this.owner.GetComponent<SkeletonAnimation>();
    }
    public void Play(string animationName, bool loop = true) {
        this.anime.state.SetAnimation(0, animationName, loop);
    }
    public void Play(string animationName, Spine.AnimationState.CompleteDelegate callback, bool loop = true) {
        if (null != callback) {
            this.anime.state.Complete += callback;
        }
        this.anime.state.SetAnimation(0, animationName, loop);
    }
}
}
