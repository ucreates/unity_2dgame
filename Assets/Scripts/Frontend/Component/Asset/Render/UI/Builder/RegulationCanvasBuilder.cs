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
    public sealed class RegulationCanvasBuilder : BaseUIAssetBuilder
    {
        private string regulation { get; set; }

        public RegulationCanvasBuilder AddRegulation(string regulation)
        {
            this.regulation = regulation;
            return this;
        }

        public override void Build()
        {
            textList.ForEach(text =>
            {
                if (text.gameObject.name.Equals("RegulationText"))
                    text.text = regulation;
            });
            Update();
        }

        public override void Update()
        {
            buttonList.ForEach(button =>
            {
                var cb = button.colors;
                cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, alpha);
                cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, alpha);
                cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b,
                    alpha);
                button.colors = cb;
                button.enabled = enabled;
            });
            imageList.ForEach(image =>
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            });
            textList.ForEach(text => { text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); });
        }
    }
}