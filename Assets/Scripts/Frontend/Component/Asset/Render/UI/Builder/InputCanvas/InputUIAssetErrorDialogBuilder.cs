//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Extensions;
using Core.Validator.Message;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class InputUIAssetErrorDialogBuilder : BaseUIAssetBuilder
    {
        private List<BaseValidateMessage> errorMessageList { get; set; } = new();

        public InputUIAssetErrorDialogBuilder AddErrorMessage(BaseValidateMessage message)
        {
            errorMessageList.Add(message);
            return this;
        }

        public InputUIAssetErrorDialogBuilder AddErrorMessage(List<BaseValidateMessage> messageList)
        {
            errorMessageList = messageList;
            return this;
        }

        public override void Build()
        {
            textList.ForEach(text =>
            {
                text.FillAlpha(alpha);
                if (!text.transform.parent.gameObject.name.Contains("Button")) errorMessageList.ForEach(error => { string.Join(text.text, $"{error.message}\n"); });
            });
            Update();
        }

        public override void Update()
        {
            buttonList.ForEach(button => { button.FillAlpha(alpha, true, true, true); });
            imageList.ForEach(image => { image.FillAlpha(alpha); });
            textList.ForEach(text => { text.FillAlpha(alpha); });
        }
    }
}