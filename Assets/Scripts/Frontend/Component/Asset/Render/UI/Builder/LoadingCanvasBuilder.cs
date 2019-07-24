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
namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class LoadingCanvasBuilder : BaseUIAssetBuilder {
    private string percentage {
        get;
        set;
    }
    public LoadingCanvasBuilder() {
    }
    public LoadingCanvasBuilder AddPercentage(string percentage) {
        this.percentage = percentage;
        return this;
    }
    public override void Build() {
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (Text text in this.textList) {
            text.text = this.percentage;
        }
        this.Update();
    }
    public override void Update() {
        foreach (Image image in this.imageList) {
            if (this.enabled) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
            } else {
                image.color = Color.white;
            }
        }
        foreach (Text text in this.textList) {
            text.text = this.percentage;
        }
    }
    public override void Reset() {
    }
}
}
