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
namespace Frontend.Component.Asset.Renderer.UI.Builder {
public sealed class RegulationCanvasBuilder : BaseUIAssetBuilder {
    private string regulation {
        get;
        set;
    }
    public RegulationCanvasBuilder() {
    }
    public RegulationCanvasBuilder AddRegulation(string regulation) {
        this.regulation = regulation;
        return this;
    }
    public override void Build() {
        foreach (Text text in this. textList) {
            if (text.gameObject.name.Equals("RegulationText")) {
                text.text = this.regulation;
            }
        }
        this.Update();
    }
    public override void Update() {
        foreach (Button button in this.buttonList) {
            ColorBlock cb = button.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, this.alpha);
            cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, this.alpha);
            cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b, this.alpha);
            button.colors = cb;
            button.enabled = this.enabled;
        }
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (Text text in this. textList) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
        }
    }
    public override void Reset() {
    }
}
}
