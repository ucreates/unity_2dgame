//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Math;
using Frontend.Component.State;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class BackGroundShowState : FiniteState<BackGroundBehaviour> {
    public override void Create() {
        if (Condition.ByRandom()) {
            return;
        }
        SpriteRenderer renderer = this.owner.GetComponent<SpriteRenderer>();
        if (null != renderer) {
            Sprite sprite = Resources.Load<Sprite>("Textures/night_back_ground");
            if (null != sprite) {
                renderer.sprite = sprite;
            }
        }
    }
}
}
