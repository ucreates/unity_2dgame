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
using Frontend.Component.Vfx;
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class InputCanvasShowState : FiniteState<InputCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; } = new();

        private InputCanvasModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            previousAlpha = 0f;
            builder ??= new InputCanvasModalDialogBuilder();
            builder?.Reset();
            var modalDialogObject = owner.transform.Find("ModalDialog");
            Image modalDialogImage = null;
            if (null != modalDialogObject)
            {
                modalDialogImage = modalDialogObject.GetComponent<Image>();
                modalDialogImage.gameObject.SetActive(true);
            }

            var errorDialigObject = owner.transform.Find("ErrorDialog");
            if (null != errorDialigObject)
            {
                var errorDialogImage = errorDialigObject.GetComponent<Image>();
                errorDialogImage.gameObject.SetActive(false);
            }

            builder
                ?.AddAlpha(0f)
                ?.AddTransform(modalDialogImage?.transform)
                ?.AddEnabled(false)
                ?.Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f);
            if (alpha < previousAlpha)
            {
                owner?.stateMachine?.Change("stay");
                return;
            }

            builder
                ?.AddAlpha(alpha)
                ?.Update();
            previousAlpha = alpha;
            alphaTimeLine?.Next(1.5f);
        }
    }
}