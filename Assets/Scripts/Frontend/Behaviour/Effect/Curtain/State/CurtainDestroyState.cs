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
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Easing;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.State;
using Service;
namespace Frontend.Behaviour.State {
public sealed class CurtainDestroyState : FiniteState<CurtainBehaviour> {
    public override void Create() {
        UnityEngine.Object.Destroy(this.owner.gameObject);
        UnityEngine.Object.Destroy(this.owner);
    }
}
}
