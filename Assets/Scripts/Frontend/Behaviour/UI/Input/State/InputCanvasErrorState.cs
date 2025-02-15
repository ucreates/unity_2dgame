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
        private TimeLine alphaTimeLine { get; set; }

        private InputUIAssetErrorDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create(object parameter)
        {
            var paramBody = (ValidatorResponse)parameter;
            previousAlpha = 0f;
            alphaTimeLine = new TimeLine();
            var md = owner.transform.Find("ModalDialog");
            var mdimg = md.GetComponent<Image>();
            mdimg.gameObject.SetActive(false);
            var ed = owner.transform.Find("ErrorDialog");
            var edimg = ed.GetComponent<Image>();
            edimg.gameObject.SetActive(true);
            if (null == builder)
                builder = new InputUIAssetErrorDialogBuilder();
            else
                builder.Reset();
            builder
                .AddErrorMessage(paramBody.GetMessageList())
                .AddAlpha(0f)
                .AddTransform(ed)
                .Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine.currentTime);
            if (alpha < previousAlpha) return;
            builder
                .AddAlpha(alpha)
                .Update();
            previousAlpha = alpha;
            alphaTimeLine.Next(1.5f);
        }
    }
}