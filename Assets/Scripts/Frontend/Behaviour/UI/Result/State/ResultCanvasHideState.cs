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
    public sealed class ResultCanvasHideState : FiniteState<ResultCanvasBehaviour> {
    public override void Create() {
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = false;
        }
    }
}
}
