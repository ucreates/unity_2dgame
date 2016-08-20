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
using Core.Validator.Message;
namespace Frontend.Component.Asset.Renderer.UI.Builder {
public sealed class InputUIAssetErrorDialogBuilder : BaseUIAssetBuilder {
    private List<BaseValidateMessage> errorMessageList {
        get;
        set;
    }
    public InputUIAssetErrorDialogBuilder() {
        this.errorMessageList = new List<BaseValidateMessage>();
    }
    public InputUIAssetErrorDialogBuilder AddErrorMessage(BaseValidateMessage message) {
        this.errorMessageList.Add(message);
        return this;
    }
    public InputUIAssetErrorDialogBuilder AddErrorMessage(List<BaseValidateMessage> messageList) {
        this.errorMessageList = messageList;
        return this;
    }
    public override void Build() {
        foreach (Text text in this.textList) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
            if (!text.transform.parent.gameObject.name.Contains("Button")) {
                text.text = string.Empty;
                foreach (BaseValidateMessage message in this.errorMessageList) {
                    text.text += message.message + "\n";
                }
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
        }
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (BaseValidateMessage message in this.errorMessageList) {
            foreach (Text text in this.textList) {
                text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
                text.text = message.message;
            }
        }
    }
    public override void Reset() {
    }
}
}
