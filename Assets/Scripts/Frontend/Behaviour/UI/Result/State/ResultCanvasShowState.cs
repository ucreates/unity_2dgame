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
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class ResultCanvasShowState : FiniteState<ResultCanvasBehaviour>
    {
        private PlayCanvasBuilder currentScoreBuilder { get; set; }

        private PlayCanvasBuilder bestScoreBuilder { get; set; }

        private ResultCanvasEmblemImageBuilder emblemBuilder { get; set; }

        private ResultCanvasBackGroundBuilder backGroundBuilder { get; set; }

        public override void Create()
        {
            var sret = ServiceGateway.GetInstance()?.Request("service://stats/result")?.Get();
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            if (null != currentScoreBuilder) currentScoreBuilder?.Reset();
            if (null != bestScoreBuilder) bestScoreBuilder?.Reset();
            if (null != emblemBuilder) emblemBuilder?.Reset();
            var clearCount = sret.Get<int>("clearcount");
            var crearCountFigure = Figure.CountFigure(clearCount);
            currentScoreBuilder = new PlayCanvasBuilder();
            currentScoreBuilder
                ?.AddClearCount(clearCount)
                ?.AddFigure(crearCountFigure)
                ?.AddSprite(owner.scoreSpriteList)
                ?.AddCanvas(canvas)
                ?.AddScale(new Vector3(0.5f, 0.5f, 1f))
                ?.AddPosition(new Vector3(65f, 25f, 0f))
                ?.Build();
            var bestClearCount = sret?.Get<int>("bestclearcount") ?? 0;
            var bestCrearCountFigure = Figure.CountFigure(bestClearCount);
            bestScoreBuilder = new PlayCanvasBuilder();
            bestScoreBuilder
                ?.AddClearCount(bestClearCount)
                ?.AddFigure(bestCrearCountFigure)
                ?.AddSprite(owner.scoreSpriteList)
                ?.AddCanvas(canvas)
                ?.AddScale(new Vector3(0.5f, 0.5f, 1f))
                ?.AddPosition(new Vector3(65f, -15f, 0f))
                ?.Build();
            emblemBuilder = new ResultCanvasEmblemImageBuilder();
            emblemBuilder
                ?.AddClearCount(clearCount)
                ?.AddSprite(owner.emblemSpriteList)
                ?.AddCanvas(canvas)
                ?.AddPosition(new Vector3(-87.5f, 5f, 0f))
                ?.Build();
            var scorePanel = owner.transform.Find("ScorePanel");
            var bg = scorePanel.GetComponent<Image>();
            var restartButton = owner.transform.Find("RestartButton");
            var rsb = restartButton.GetComponent<Button>();
            var rankingButton = owner.transform.Find("RankingButton");
            var rkb = rankingButton.GetComponent<Button>();
            backGroundBuilder = new ResultCanvasBackGroundBuilder();
            backGroundBuilder
                ?.AddImage(bg)
                ?.AddButton(rsb)
                ?.AddButton(rkb)
                ?.Build();
        }
    }
}