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
        private TimeLine alphaTimeLine { get; set; }

        private InputCanvasModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            previousAlpha = 0f;
            alphaTimeLine = new TimeLine();
            if (null == builder)
                builder = new InputCanvasModalDialogBuilder();
            else
                builder?.Reset();
            var md = owner.transform.Find("ModalDialog");
            if (null != md)
            {
                var mdimg1 = md.GetComponent<Image>();
                mdimg1.gameObject.SetActive(true);
            }

            var ed = owner.transform.Find("ErrorDialog");
            if (null != ed)
            {
                var edimg = ed.GetComponent<Image>();
                edimg.gameObject.SetActive(false);
            }

            var mdtr = owner.transform.Find("ModalDialog");
            var mdimg2 = mdtr.GetComponent<Image>();
            builder
                ?.AddAlpha(0f)
                ?.AddTransform(mdimg2.transform)
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