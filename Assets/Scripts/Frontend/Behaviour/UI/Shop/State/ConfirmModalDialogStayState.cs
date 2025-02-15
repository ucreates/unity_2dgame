//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class ConfirmModalDialogStayState : FiniteState<ShopCanvasBehaviour>
    {
        private ShopCanvasConfirmModalDialogBuilder builder { get; set; }

        public override void Create(object paramter)
        {
            if (null == builder)
                builder = new ShopCanvasConfirmModalDialogBuilder();
            else
                builder.Reset();
            var roottrsfrm = owner.transform.Find("ConfirmModalDialog");
            builder
                .AddAlpha(1f)
                .AddTransform(roottrsfrm)
                .AddEnabled(true)
                .Update();
        }
    }
}