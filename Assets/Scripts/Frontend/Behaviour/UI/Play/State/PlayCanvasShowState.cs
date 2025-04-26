//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Math;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Service;
using Service.Strategy;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class PlayCanvasShowState : FiniteState<PlayCanvasBehaviour>
    {
        private BaseStrategy strategy { get; set; }

        private PlayCanvasBuilder builder { get; set; }

        public override void Create()
        {
            strategy = ServiceGateway.GetInstance()?.Request("service://stats/player");
            strategy?.Clear();
            var response = strategy?.Get();
            var data = ((int clearCount, string nickname, string copylight))response?.data;
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            if (null == builder)
                builder = new PlayCanvasBuilder();
            else
                builder?.Reset();
            builder
                ?.AddNickName(data.nickname)
                ?.AddCopyright(data.copylight)
                ?.AddSprite(owner.scoreSpriteList)
                ?.AddTransform(owner.transform)
                ?.AddPosition(new Vector3(0f, 250f, 0f))
                ?.Build();
        }

        public override void Update()
        {
            var response = strategy.Get();
            var data = ((int clearCount, string nickname, string copylight))response?.data;
            var figure = Figure.CountFigure(data.clearCount);
            builder
                ?.AddClearCount(data.clearCount)
                ?.AddFigure(figure)
                ?.Update();
        }
    }
}