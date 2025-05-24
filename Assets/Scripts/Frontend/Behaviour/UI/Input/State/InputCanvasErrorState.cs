//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Validator.Entity;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class InputCanvasErrorState : FiniteState<InputCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; } = new();

        private InputUIAssetErrorDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create(object parameter)
        {
            var validatorResponse = (ValidatorResponse)parameter;
            previousAlpha = 0f;
            var modalDialogObject = owner.transform.Find("ModalDialog");
            var modalDialogImage = modalDialogObject.GetComponent<Image>();
            modalDialogImage.gameObject.SetActive(false);
            var errorDialogObject = owner.transform.Find("ErrorDialog");
            var errorDialogImage = errorDialogObject.GetComponent<Image>();
            errorDialogImage.gameObject.SetActive(true);
            builder ??= new InputUIAssetErrorDialogBuilder();
            builder?.Reset();
            builder
                ?.AddErrorMessage(validatorResponse.GetMessageList())
                ?.AddAlpha(0f)
                ?.AddTransform(errorDialogObject)
                ?.Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f);
            if (alpha < previousAlpha) return;
            builder
                ?.AddAlpha(alpha)
                ?.Update();
            previousAlpha = alpha;
            alphaTimeLine.Next(1.5f);
        }
    }
}