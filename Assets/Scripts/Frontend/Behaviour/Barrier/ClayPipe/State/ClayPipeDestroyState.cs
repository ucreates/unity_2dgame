﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.State;
namespace Frontend.Behaviour.State
{
    public sealed class ClayPipeDestroyState : FiniteState<ClayPipeBehaviour> {
    public override void Create() {
        UnityEngine.GameObject.Destroy(this.owner.gameObject);
        UnityEngine.GameObject.Destroy(this.owner);
    }
}
}
