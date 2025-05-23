﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class RankingCanvasBuilder : BaseUIAssetBuilder
    {
        private List<MUserTable> userTableList { get; set; } = new();

        private List<TScoreTable> scoreTableList { get; set; } = new();

        public RankingCanvasBuilder AddUserTableList(List<MUserTable> userTableList)
        {
            this.userTableList = userTableList;
            return this;
        }

        public RankingCanvasBuilder AddScoreTableList(List<TScoreTable> scoreTableList)
        {
            this.scoreTableList = scoreTableList;
            return this;
        }

        public override void Build()
        {
            var nextY = 0f;
            var size = spriteList.FirstOrDefault()?.rect.size ?? Vector2.zero;
            scoreTableList.For((i, score) =>
                {
                    var spriteIndex = i + 1;
                    var rankingImageObject = new GameObject("RankingImage");
                    var rankingImage = rankingImageObject.AddComponent<Image>();
                    rankingImage.transform.SetParent(canvas.transform);
                    rankingImage.rectTransform.sizeDelta = size;
                    rankingImage.rectTransform.localScale = scale;
                    rankingImage.rectTransform.anchoredPosition = position + new Vector3(0f, -nextY, 0f);
                    rankingImage.sprite = spriteList[spriteIndex];
                    imageList.Add(rankingImage);
                    var user = userTableList[i];
                    var rankingTextObject = new GameObject("RankingText");
                    var rankingText = rankingTextObject.AddComponent<Text>();
                    rankingText.transform.SetParent(canvas.transform);
                    rankingText.rectTransform.anchoredPosition = position + new Vector3(60f, -nextY, 10f);
                    rankingText.rectTransform.localScale = scale;
                    rankingText.rectTransform.sizeDelta = new Vector2(100f, 30f);
                    rankingText.fontSize = 14;
                    rankingText.alignment = TextAnchor.MiddleLeft;
                    rankingText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf");
                    rankingText.fontStyle = FontStyle.Normal;
                    rankingText.text = $"{user.nickName} {score.clearCount} points.";
                    rankingText.color = Color.black;
                    rankingText.horizontalOverflow = HorizontalWrapMode.Wrap;
                    rankingText.verticalOverflow = VerticalWrapMode.Overflow;
                    textList.Add(rankingText);
                    nextY += size.y + 10f;
                }
            );

            Update();
        }

        public override void Update()
        {
            textList.For((i, rankingText) =>
            {
                var score = scoreTableList[i];
                var user = userTableList[i];
                rankingText.text = $"{user.nickName} {score.clearCount} points.";
            });

            buttonList.ForEach(button =>
            {
                button.FillAlpha(alpha, true, true, true);
                button.enabled = enabled;
            });
            imageList.ForEach(image => { image.FillAlpha(alpha); });
            textList.ForEach(text => { text.FillAlpha(alpha); });
        }

        public override void Reset()
        {
            alpha = 0f;
            enabled = false;
            Update();
        }
    }
}