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
    public sealed class InputCanvasModalDialogBuilder : BaseUIAssetBuilder {
    public InputCanvasModalDialogBuilder() {
    }
    public override void Build() {
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
        foreach (InputField input in this.inputFieldList) {
            ColorBlock cb = input.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, this.alpha);
            cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, this.alpha);
            cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b, this.alpha);
            cb.disabledColor = new Color(cb.disabledColor.r, cb.disabledColor.g, cb.disabledColor.b, this.alpha);
            input.colors = cb;
            input.enabled = this.enabled;
        }
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (Text text in this. textList) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
        }
        foreach (Toggle toggle in this.toggleList) {
            ColorBlock tcb = toggle.colors;
            tcb.normalColor = new Color(tcb.normalColor.r, tcb.normalColor.g, tcb.normalColor.b, this.alpha);
            tcb.pressedColor = new Color(tcb.pressedColor.r, tcb.pressedColor.g, tcb.pressedColor.b, this.alpha);
            tcb.highlightedColor = new Color(tcb.highlightedColor.r, tcb.highlightedColor.g, tcb.highlightedColor.b, this.alpha);
            toggle.colors = tcb;
            toggle.enabled = this.enabled;
        }
    }
    public override void Reset() {
    }
}
}
