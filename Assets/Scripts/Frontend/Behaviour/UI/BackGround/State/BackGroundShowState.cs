//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Utility;
using Frontend.Component.State;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class BackGroundShowState : FiniteState<BackGroundBehaviour>
    {
        public override void Create()
        {
            if (ConditionUtility.ByRandom()) return;
            var renderer = owner.GetComponent<SpriteRenderer>();
            if (null != renderer)
            {
                var sprite = Resources.Load<Sprite>("Textures/night_back_ground");
                if (null != sprite) renderer.sprite = sprite;
            }
        }
    }
}