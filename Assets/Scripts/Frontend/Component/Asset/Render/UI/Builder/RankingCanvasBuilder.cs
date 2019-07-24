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
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.UI;
namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class RankingCanvasBuilder : BaseUIAssetBuilder {
    private string copyright {
        get;
        set;
    }
    private List<MUserTable> userTableList {
        get;
        set;
    }
    private List<TScoreTable> scoreTableList {
        get;
        set;
    }
    public RankingCanvasBuilder() {
        this.scoreTableList = new List<TScoreTable>();
        this.userTableList = new List<MUserTable>();
    }
    public RankingCanvasBuilder AddUserTableList(List<MUserTable> userTableList) {
        this.userTableList = userTableList;
        return this;
    }
    public RankingCanvasBuilder AddScoreTableList(List<TScoreTable> scoreTableList) {
        this.scoreTableList = scoreTableList;
        return this;
    }
    public override void Build() {
        float ny = 0f;
        Vector2 size = spriteList[0].rect.size;
        for (int i = 0; i < this.scoreTableList.Count; i++) {
            int spriteIndex = i + 1;
            GameObject image = new GameObject("RankingImage");
            Image rankingImage = image.AddComponent<Image>();
            rankingImage.transform.SetParent(this.canvas.transform);
            rankingImage.rectTransform.sizeDelta = size;
            rankingImage.rectTransform.localScale = this.scale;
            rankingImage.rectTransform.anchoredPosition = this.position + new Vector3(0f, -ny, 0f);
            rankingImage.sprite = spriteList[spriteIndex];
            this.imageList.Add(rankingImage);
            TScoreTable score = this.scoreTableList[i];
            MUserTable user = this.userTableList[i];
            GameObject text = new GameObject("RankingText");
            Text rankingText = text.AddComponent<Text>();
            rankingText.transform.SetParent(this.canvas.transform);
            rankingText.rectTransform.anchoredPosition = this.position + new Vector3(60f, -ny, 10f);
            rankingText.rectTransform.localScale = this.scale;
            rankingText.rectTransform.sizeDelta = new Vector2(100f, 30f);
            rankingText.fontSize = 14;
            rankingText.alignment = TextAnchor.MiddleLeft;
            rankingText.font  = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            rankingText.fontStyle = FontStyle.Normal;
            rankingText.text = user.nickName + " " + score.clearCount.ToString() + " points.";
            rankingText.color = Color.black;
            rankingText.horizontalOverflow = HorizontalWrapMode.Wrap;
            rankingText.verticalOverflow = VerticalWrapMode.Overflow;
            this.textList.Add(rankingText);
            ny += size.y + 10f;
        }
        this.Update();
    }
    public override void Update() {
        for (int i = 0; i < this.textList.Count; i++) {
            Text rankingText = this.textList[i];
            TScoreTable score = this.scoreTableList[i];
            MUserTable user = this.userTableList[i];
            rankingText.text = user.nickName + " " + score.clearCount.ToString() + " points.";
        }
        foreach (Button button in this.buttonList) {
            ColorBlock cb = button.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, this.alpha);
            cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, this.alpha);
            cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b, this.alpha);
            button.colors = cb;
            button.enabled = this.enabled;
        }
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (Text text in this. textList) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
        }
    }
    public override void Reset() {
        this.alpha = 0f;
        this.enabled = false;
        this.Update();
    }
}
}
