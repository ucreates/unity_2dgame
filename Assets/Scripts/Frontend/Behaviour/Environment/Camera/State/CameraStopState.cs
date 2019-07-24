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
    public sealed class CameraStopState : FiniteState<CameraBehaviour> {
    public override void Create() {
        Camera.main.orthographicSize = CameraBehaviour.DEFAULT_OTHROGRAPHIC_SIZE;
    }
}
}
