//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.State;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class CurtainShowState : FiniteState<CurtainBehaviour> {
    public override void Create() {
        SpriteRenderer sr = this.owner.GetComponent<SpriteRenderer>();
        var color = sr.color;
        color.a = 1f;
        sr.color = color;
    }
}
}
