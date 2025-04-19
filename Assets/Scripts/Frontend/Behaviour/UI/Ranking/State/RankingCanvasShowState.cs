//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Service;
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Behaviour.State
{
    public sealed class RankingCanvasShowState : FiniteState<RankingCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private RankingCanvasBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            var response = ServiceGateway.GetInstance()?.Request("service://stats/ranking")?.Get();
            var data = ((List<TScoreTable> rankingList, List<MUserTable> userList))response?.data;
            if (null == builder)
            {
                var campanynametrsfrm = owner.transform.Find("BackGroundImage");
                var bgImage = campanynametrsfrm.GetComponent<Image>();
                var sb = owner.transform.Find("ConfirmButton");
                var confirmButton = sb.GetComponent<Button>();
                builder = new RankingCanvasBuilder();
                builder
                    ?.AddScoreTableList(data.rankingList)
                    ?.AddUserTableList(data.userList)
                    ?.AddSprite(owner.scoreSpriteList)
                    ?.AddCanvas(canvas)
                    ?.AddImage(bgImage)
                    ?.AddButton(confirmButton)
                    ?.AddScale(Vector3.one * 0.8f)
                    ?.AddPosition(new Vector3(-50f, 100f, 0f))
                    ?.AddAlpha(0f)
                    ?.AddEnabled(false)
                    ?.Build();
            }
            else
            {
                builder
                    ?.AddScoreTableList(data.rankingList)
                    ?.AddUserTableList(data.userList)
                    ?.Reset();
            }

            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
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