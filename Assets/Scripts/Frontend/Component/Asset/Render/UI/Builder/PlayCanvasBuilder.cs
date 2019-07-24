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
using Core.Math;
using UnityEngine;
using UnityEngine.UI;
namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class PlayCanvasBuilder : BaseUIAssetBuilder {
    private List<Image> clearCountImageList {
        get;
        set;
    }
    private int clearCount {
        get;
        set;
    }
    private int figure {
        get;
        set;
    }
    private string nickName {
        get;
        set;
    }
    private string copyright {
        get;
        set;
    }
    public PlayCanvasBuilder() {
        this.clearCountImageList = new List<Image>();
        this.position = Vector3.one;
        this.scale = Vector3.one;
        this.nickName = string.Empty;
    }
    public PlayCanvasBuilder AddClearCount(int clearCount) {
        this.clearCount = clearCount;
        return this;
    }
    public PlayCanvasBuilder AddFigure(int figure) {
        this.figure = figure;
        return this;
    }
    public PlayCanvasBuilder AddNickName(string nickName) {
        this.nickName = nickName;
        return this;
    }
    public PlayCanvasBuilder AddCopyright(string copyright) {
        this.copyright = copyright;
        return this;
    }
    public override void Build() {
        this.Update();
        foreach (Text text in this.textList) {
            string objName = text.name.ToLower();
            if (objName.Equals("nicknametext")) {
                text.text = this.nickName;
            } else if (objName.Equals("copyrighttext")) {
                text.text = this.copyright;
            }
        }
    }
    public override void Update() {
        Vector2 size = spriteList[0].rect.size;
        if (this.figure > this.clearCountImageList.Count) {
            int addCount = this.figure - this.clearCountImageList.Count;
            for (int i = 0; i < addCount; i++) {
                GameObject image = new GameObject("ClearCount");
                Image clearCountImage = image.AddComponent<Image>();
                clearCountImage.transform.SetParent(this.canvas.transform);
                clearCountImage.rectTransform.sizeDelta = size;
                clearCountImage.rectTransform.localScale = this.scale;
                clearCountImage.rectTransform.anchoredPosition = Vector3.zero;
                this.clearCountImageList.Add(clearCountImage);
                this.imageList.Add(clearCountImage);
            }
        }
        float totalWidth = this.figure * spriteList[0].rect.size.x * this.scale.x;
        float ex = (totalWidth / 2f);
        for (int i = 1; i <= this.figure; i++) {
            int value = Figure.GetSpeciFigureValue(this.clearCount, i);
            int index = i - 1;
            Sprite sprite = this.spriteList[value];
            this.clearCountImageList[index].sprite = sprite;
            this.clearCountImageList[index].rectTransform.anchoredPosition = new Vector3(ex + this.position.x , this.position.y, 0f);
            ex -= size.x * this.scale.x;
        }
    }
    public override void Reset() {
        GameObject canvas = GameObject.Find(this.canvas.name);
        if (null != canvas) {
            foreach (Transform child in canvas.transform) {
                if (child.gameObject.name.Equals("ClearCount")) {
                    UnityEngine.GameObject.Destroy(child.gameObject);
                    UnityEngine.GameObject.Destroy(child);
                }
            }
        }
        this.imageList.Clear();
        this.clearCountImageList.Clear();
        this.figure = 0;
    }
}
}
