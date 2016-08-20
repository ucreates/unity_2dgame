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
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Core.Math;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Service;
namespace Frontend.Behaviour.State {
public sealed class InputCanvasStayState : FiniteState<InputCanvasBehaviour> {
    private InputCanvasModalDialogBuilder builder {
        get;
        set;
    }
    public override void Create() {
        if (null == this.builder) {
            this.builder = new InputCanvasModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        Transform mdtr = this.owner.transform.FindChild("ModalDialog");
        Image modalDialogBG = mdtr.GetComponent<Image>();
        this.builder
        .AddAlpha(1f)
        .AddTransform(modalDialogBG.transform)
        .AddEnabled(true)
        .Build();
    }
}
}
