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
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class InputCanvasStayState : FiniteState<InputCanvasBehaviour>
    {
        private InputCanvasModalDialogBuilder builder { get; set; }

        public override void Create()
        {
            if (null == builder)
                builder = new InputCanvasModalDialogBuilder();
            else
                builder.Reset();
            var mdtr = owner.transform.Find("ModalDialog");
            var modalDialogBG = mdtr.GetComponent<Image>();
            builder
                .AddAlpha(1f)
                .AddTransform(modalDialogBG.transform)
                .AddEnabled(true)
                .Build();
        }
    }
}