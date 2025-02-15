//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Extensions;

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
                button.FillAlpha(alpha, true, true, true);
                button.enabled = enabled;
            });
            imageList.ForEach(image => { image.FillAlpha(alpha); });
            textList.ForEach(text => { text.FillAlpha(alpha); });
        }
    }
}