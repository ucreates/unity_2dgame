//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Notify;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class ReadyCanvasShowState : FiniteState<ReadyCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; } = new();

        private Image getReadyImage { get; set; }

        private Image tapLogoImage { get; set; }

        public override void Create()
        {
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            getReadyImage = GameObject.Find("GetReadyImage").GetComponent<Image>();
            tapLogoImage = GameObject.Find("TapScreenLogoImage").GetComponent<Image>();
        }

        public override void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f, 2f);
                var scale = Flash.Update(alphaTimeLine?.currentTime ?? 0f, 7.0f, 0.1f);
                getReadyImage.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                tapLogoImage.rectTransform.localScale = new Vector2(1f + scale, 1f + scale);
                alphaTimeLine?.Next();
            }
            else
            {
                owner?.stateMachine?.Change("hide");
                var notifier = Notifier.GetInstance();
                if (notifier.currentMessage.title != NotifyMessage.Title.GameStart) notifier?.Notify(NotifyMessage.Title.GameStart);
            }
        }
    }
}