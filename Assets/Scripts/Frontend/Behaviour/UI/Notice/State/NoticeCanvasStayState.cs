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
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class NoticeCanvasStayState : FiniteState<NoticeCanvasBehaviour>
    {
        private NoticeCanvasBuilder builder { get; set; }

        public override void Create()
        {
            if (null == builder)
                builder = new NoticeCanvasBuilder();
            else
                builder.Reset();
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            builder = new NoticeCanvasBuilder();
            builder
                .AddTransform(owner.transform)
                .AddAlpha(1f)
                .AddEnabled(true)
                .Update();
        }
    }
}