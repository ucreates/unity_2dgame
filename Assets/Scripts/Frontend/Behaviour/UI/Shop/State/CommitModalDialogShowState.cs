//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Core.Extensions;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class CommitModalDialogShowState : FiniteState<ShopCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private ShopCanvasListModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        private Parameter notifyParameter { get; set; }

        public override void Create(Parameter paramter)
        {
            notifyParameter = paramter;
            var message = paramter.Get<string>("message");
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
            owner.transform.ForEach(child =>
            {
                if (child.name.Equals("CommitModalDialog"))
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            });
            if (null == builder)
                builder = new ShopCanvasListModalDialogBuilder();
            else
                builder.Reset();
            builder
                .AddCommitMessage(message)
                .AddTransform(owner.transform)
                .AddAlpha(0f)
                .AddEnabled(false)
                .Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine.currentTime);
            if (alpha < previousAlpha)
            {
                owner.stateMachine.Change("commitstay", notifyParameter);
                return;
            }

            builder
                .AddAlpha(alpha)
                .Update();
            previousAlpha = alpha;
            alphaTimeLine.Next(1.5f);
        }
    }
}