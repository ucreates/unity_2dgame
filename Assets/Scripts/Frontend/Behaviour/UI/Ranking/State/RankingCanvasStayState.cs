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
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class RankingCanvasStayState : FiniteState<RankingCanvasBehaviour>
    {
        private RankingCanvasBuilder builder { get; set; }

        public override void Create()
        {
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            var confirmButtonObject = owner.transform.Find("ConfirmButton");
            var confirmButton = confirmButtonObject.GetComponent<Button>();
            builder = new RankingCanvasBuilder();
            builder
                ?.AddButton(confirmButton)
                ?.AddAlpha(1f)
                ?.AddEnabled(true)
                ?.Update();
        }
    }
}