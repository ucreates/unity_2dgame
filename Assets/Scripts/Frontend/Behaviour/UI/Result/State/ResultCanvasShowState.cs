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
using Core.Math;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Service;
using UnityEngine;
using UnityEngine.UI;
namespace Frontend.Behaviour.State
{
    public sealed class ResultCanvasShowState : FiniteState<ResultCanvasBehaviour> {
    private PlayCanvasBuilder currentScoreBuilder {
        get;
        set;
    }
    private PlayCanvasBuilder bestScoreBuilder {
        get;
        set;
    }
    private ResultCanvasEmblemImageBuilder emblemBuilder {
        get;
        set;
    }
    private ResultCanvasBackGroundBuilder backGroundBuilder {
        get;
        set;
    }
    public override void Create() {
        Response sret = ServiceGateway.GetInstance().Request("service://stats/result").Get();
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        if (null != this.currentScoreBuilder) {
            this.currentScoreBuilder.Reset();
        }
        if (null != this.bestScoreBuilder) {
            this.bestScoreBuilder.Reset();
        }
        if (null != this.emblemBuilder) {
            this.emblemBuilder.Reset();
        }
        int clearCount = sret.Get<int>("clearcount");
        int crearCountFigure = Figure.CountFigure(clearCount);
        this.currentScoreBuilder = new PlayCanvasBuilder();
        currentScoreBuilder
        .AddClearCount(clearCount)
        .AddFigure(crearCountFigure)
        .AddSprite(this.owner.scoreSpriteList)
        .AddCanvas(canvas)
        .AddScale(new Vector3(0.5f, 0.5f, 1f))
        .AddPosition(new Vector3(65f, 25f, 0f))
        .Build();
        int bestClearCount = sret.Get<int>("bestclearcount");
        int bestCrearCountFigure = Figure.CountFigure(bestClearCount);
        this.bestScoreBuilder = new PlayCanvasBuilder();
        bestScoreBuilder
        .AddClearCount(bestClearCount)
        .AddFigure(bestCrearCountFigure)
        .AddSprite(this.owner.scoreSpriteList)
        .AddCanvas(canvas)
        .AddScale(new Vector3(0.5f, 0.5f, 1f))
        .AddPosition(new Vector3(65f, -15f, 0f))
        .Build();
        this.emblemBuilder = new ResultCanvasEmblemImageBuilder();
        this.emblemBuilder
        .AddClearCount(clearCount)
        .AddSprite(this.owner.emblemSpriteList)
        .AddCanvas(canvas)
        .AddPosition(new Vector3(-87.5f, 5f, 0f))
        .Build();
        Transform scorePanel = this.owner.transform.Find("ScorePanel");
        Image bg = scorePanel.GetComponent<Image>();
        Transform restartButton = this.owner.transform.Find("RestartButton");
        Button rsb = restartButton.GetComponent<Button>();
        Transform rankingButton = this.owner.transform.Find("RankingButton");
        Button rkb = rankingButton.GetComponent<Button>();
        this.backGroundBuilder = new ResultCanvasBackGroundBuilder();
        this.backGroundBuilder
        .AddImage(bg)
        .AddButton(rsb)
        .AddButton(rkb)
        .Build();
    }
}
}
