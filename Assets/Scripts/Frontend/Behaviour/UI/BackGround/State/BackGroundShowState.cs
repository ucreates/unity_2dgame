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
using Service;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.Vfx.Easing;
using Frontend.Component.State;
using Core.Utility;
namespace Frontend.Behaviour.State {
public sealed class BackGroundShowState : FiniteState<BackGroundBehaviour> {
    public override void Create() {
        if (ConditionUtility.ByRandom()) {
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
