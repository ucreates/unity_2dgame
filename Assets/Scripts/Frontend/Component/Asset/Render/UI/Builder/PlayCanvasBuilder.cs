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
using System.Linq;
using Core.Extensions;
using Core.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class PlayCanvasBuilder : BaseUIAssetBuilder
    {
        public PlayCanvasBuilder()
        {
            clearCountImageList = new List<Image>();
            position = Vector3.one;
            scale = Vector3.one;
            nickName = string.Empty;
        }

        private List<Image> clearCountImageList { get; }

        private int clearCount { get; set; }

        private int figure { get; set; }

        private string nickName { get; set; }

        private string copyright { get; set; }

        public PlayCanvasBuilder AddClearCount(int clearCount)
        {
            this.clearCount = clearCount;
            return this;
        }

        public PlayCanvasBuilder AddFigure(int figure)
        {
            this.figure = figure;
            return this;
        }

        public PlayCanvasBuilder AddNickName(string nickName)
        {
            this.nickName = nickName;
            return this;
        }

        public PlayCanvasBuilder AddCopyright(string copyright)
        {
            this.copyright = copyright;
            return this;
        }

        public override void Build()
        {
            Update();
            textList.ForEach(text =>
            {
                var textName = text.name.ToLower();
                if (textName.Equals("nicknametext")) text.text = nickName;
                else if (textName.Equals("copyrighttext")) text.text = copyright;
            });
        }

        public override void Update()
        {
            var size = spriteList.FirstOrDefault()?.rect.size ?? Vector2.zero;
            if (figure > clearCountImageList.Count)
            {
                var addCount = figure - clearCountImageList.Count;
                for (var i = 0; i < addCount; i++)
                {
                    var image = new GameObject("ClearCount");
                    var clearCountImage = image.AddComponent<Image>();
                    clearCountImage.transform.SetParent(canvas.transform);
                    clearCountImage.rectTransform.sizeDelta = size;
                    clearCountImage.rectTransform.localScale = scale;
                    clearCountImage.rectTransform.anchoredPosition = Vector3.zero;
                    clearCountImageList.Add(clearCountImage);
                    imageList.Add(clearCountImage);
                }
            }

            var totalWidth = figure * spriteList.FirstOrDefault()?.rect.size.x * scale.x ?? 0f;
            var ex = totalWidth / 2f;
            for (var i = 1; i <= figure; i++)
            {
                var value = Figure.GetSpeciFigureValue(clearCount, i);
                var index = i - 1;
                var sprite = spriteList[value];
                clearCountImageList[index].sprite = sprite;
                clearCountImageList[index].rectTransform.anchoredPosition = new Vector3(ex + position.x, position.y, 0f);
                ex -= size.x * scale.x;
            }
        }

        public override void Reset()
        {
            var canvas = GameObject.Find(this.canvas.name);
            if (null != canvas)
                canvas.transform.ForEach(child =>
                {
                    if (child.gameObject.name.Equals("ClearCount"))
                    {
                        GameObject.Destroy(child.gameObject);
                        GameObject.Destroy(child);
                    }
                });

            imageList.Clear();
            clearCountImageList.Clear();
            figure = 0;
        }
    }
}