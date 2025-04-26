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
            var response = ServiceGateway.GetInstance()?.Request("service://stats/result")?.Get();
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            if (null != currentScoreBuilder) currentScoreBuilder?.Reset();
            if (null != bestScoreBuilder) bestScoreBuilder?.Reset();
            if (null != emblemBuilder) emblemBuilder?.Reset();
            var data = ((int clearCount, int bestClearCount))response?.data;
            var clearCount = data.clearCount;
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
            var bestClearCount = data.bestClearCount;
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
            var scorePanelObject = owner.transform.Find("ScorePanel");
            var bgImage = scorePanelObject.GetComponent<Image>();
            var restartButtonObject = owner.transform.Find("RestartButton");
            var resutartButton = restartButtonObject.GetComponent<Button>();
            var rankingButtonObject = owner.transform.Find("RankingButton");
            var rankingButton = rankingButtonObject.GetComponent<Button>();
            backGroundBuilder = new ResultCanvasBackGroundBuilder();
            backGroundBuilder
                ?.AddImage(bgImage)
                ?.AddButton(resutartButton)
                ?.AddButton(rankingButton)
                ?.Build();
        }
    }
}