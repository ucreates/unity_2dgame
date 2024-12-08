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
    public sealed class LoadingCanvasBuilder : BaseUIAssetBuilder
    {
        private string percentage { get; set; }

        public LoadingCanvasBuilder AddPercentage(string percentage)
        {
            this.percentage = percentage;
            return this;
        }

        public override void Build()
        {
            imageList.ForEach(image => { image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); });
            textList.ForEach(text => { text.text = percentage; });
            Update();
        }

        public override void Update()
        {
            imageList.ForEach(image =>
            {
                if (enabled)
                    image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                else
                    image.color = Color.white;
            });
            textList.ForEach(text => { text.text = percentage; });
        }
    }
}