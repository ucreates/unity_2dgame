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
using Frontend.Component.Asset.Renderer.Animator.Builder;
namespace Frontend.Behaviour.State {
public sealed class CameraStopState : FiniteState<CameraBehaviour> {
    public override void Create() {
        Camera.main.orthographicSize = CameraBehaviour.DEFAULT_OTHROGRAPHIC_SIZE;
    }
}
}
