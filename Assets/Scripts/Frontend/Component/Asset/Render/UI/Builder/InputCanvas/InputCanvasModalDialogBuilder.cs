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

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class InputCanvasModalDialogBuilder : BaseUIAssetBuilder
    {
        public override void Build()
        {
            Update();
        }

        public override void Update()
        {
            buttonList.ForEach(button =>
            {
                var colors = button.colors;
                colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, alpha);
                colors.pressedColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b, alpha);
                colors.highlightedColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, alpha);
                button.colors = colors;
                button.enabled = enabled;
            });
            inputFieldList.ForEach(input =>
            {
                var colors = input.colors;
                colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, alpha);
                colors.pressedColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b, alpha);
                colors.highlightedColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, alpha);
                colors.disabledColor = new Color(colors.disabledColor.r, colors.disabledColor.g, colors.disabledColor.b, alpha);
                input.colors = colors;
                input.enabled = enabled;
            });
            imageList.ForEach(image => { image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); });
            textList.ForEach(text => { text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); });
            toggleList.ForEach(toggle =>
            {
                var colors = toggle.colors;
                colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, alpha);
                colors.pressedColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b, alpha);
                colors.highlightedColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, alpha);
                toggle.colors = colors;
                toggle.enabled = enabled;
            });
        }
    }
}