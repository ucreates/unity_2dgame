//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Service;
using Service.Strategy;
using Service.Integration;
using Service.Integration.Table;
namespace Frontend.Behaviour.State {
public sealed class RankingCanvasShowState : FiniteState<RankingCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private RankingCanvasBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    public override void Create() {
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        Response response = ServiceGateway.GetInstance().Request("service://stats/ranking").Get();
        List<TScoreTable> rankingList = response.Get<List<TScoreTable>>("rankinglist");
        List<MUserTable> userList = response.Get<List<MUserTable>>("userlist");
        if (null == this.builder) {
            Transform campanynametrsfrm = this.owner.transform.FindChild("BackGroundImage");
            Image bgImage = campanynametrsfrm.GetComponent< Image>();
            Transform sb = this.owner.transform.FindChild("ConfirmButton");
            Button confirmButton = sb.GetComponent<Button>();
            this.builder = new RankingCanvasBuilder();
            this.builder
            .AddScoreTableList(rankingList)
            .AddUserTableList(userList)
            .AddSprite(this.owner.scoreSpriteList)
            .AddCanvas(canvas)
            .AddImage(bgImage)
            .AddButton(confirmButton)
            .AddScale(Vector3.one * 0.8f)
            .AddPosition(new Vector3(-50f, 100f, 0f))
            .AddAlpha(0f)
            .AddEnabled(false)
            .Build();
        } else {
            this.builder
            .AddScoreTableList(rankingList)
            .AddUserTableList(userList)
            .Reset();
        }
        this.alphaTimeLine = new TimeLine();
        this.previousAlpha = 0f;
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("stay");
            return;
        }
        this.builder
        .AddAlpha(alpha)
        .Update();
        this.previousAlpha = alpha;
        this.alphaTimeLine.Next(1.5f);
    }
}
}
