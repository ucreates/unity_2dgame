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
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class NoticeCanvasShowState : FiniteState<NoticeCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private NoticeCanvasBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            owner.webViewPlugin?.Show("http://u-creates.com/template/notice/", Screen.width * 0.3f, Screen.height * 0.3f,
                Screen.width * 0.3f, Screen.height * 0.35f);
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
            builder = new NoticeCanvasBuilder();
            builder
                ?.AddTransform(owner.transform)
                ?.AddAlpha(0f)
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
            alphaTimeLine.Next(1.5f);
        }
    }
}