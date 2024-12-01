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
using Core.Math;
namespace Frontend.Component.Asset.Renderer.UI.Builder {
public sealed class ResultCanvasEmblemImageBuilder : BaseUIAssetBuilder {
    private const int WHITE_MEDAL_SCORE = 0;
    private const int BRONZE_MEDAL_SCORE = 25;
    private const int SILVER_MEDAL_SCORE = 50;
    private Image emblemImage {
        get;
        set;
    }
    private int clearCount {
        get;
        set;
    }
    public ResultCanvasEmblemImageBuilder() {
        this.position = Vector3.one;
        this.scale = Vector3.one;
    }
    public ResultCanvasEmblemImageBuilder AddClearCount(int clearCount) {
        this.clearCount = clearCount;
        return this;
    }
    public override void Build() {
        Vector2 size = spriteList[0].rect.size;
        GameObject image = new GameObject("Emblem");
        this.emblemImage = image.AddComponent<Image>();
        this.emblemImage.rectTransform.SetParent(this.canvas.transform);
        this.emblemImage.rectTransform.sizeDelta = size;
        this.emblemImage.rectTransform.localScale = this.scale;
        this.emblemImage.rectTransform.anchoredPosition = Vector3.zero;
        float totalWidth = spriteList[0].rect.size.x * this.scale.x;
        float sx = (totalWidth / 2f);
        Sprite sprite = this.spriteList[0];
        if (ResultCanvasEmblemImageBuilder.WHITE_MEDAL_SCORE < this.clearCount && this.clearCount <= ResultCanvasEmblemImageBuilder.BRONZE_MEDAL_SCORE) {
            sprite = this.spriteList[3];
        } else if (ResultCanvasEmblemImageBuilder.BRONZE_MEDAL_SCORE < this.clearCount && this.clearCount <= ResultCanvasEmblemImageBuilder.SILVER_MEDAL_SCORE) {
            sprite = this.spriteList[2];
        } else if (ResultCanvasEmblemImageBuilder.SILVER_MEDAL_SCORE < this.clearCount) {
            sprite = this.spriteList[1];
        }
        this.emblemImage.sprite = sprite;
        this.emblemImage.rectTransform.anchoredPosition = new Vector3(sx + this.position.x , this.position.y, 0f);
        this.imageList.Add(this.emblemImage);
    }
    public override void Reset() {
        UnityEngine.GameObject.Destroy(this.emblemImage.gameObject);
        UnityEngine.GameObject.Destroy(this.emblemImage);
    }
}
}
