﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Extensions;
using Frontend.Component.State;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class CurtainShowState : FiniteState<CurtainBehaviour>
    {
        public override void Create()
        {
            var renderer = owner.GetComponent<SpriteRenderer>();
            renderer?.FillAlpha(1f);
        }
    }
}