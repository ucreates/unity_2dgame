//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class ResultCanvasEmblemImageBuilder : BaseUIAssetBuilder
    {
        private const int WHITE_MEDAL_SCORE = 0;
        private const int BRONZE_MEDAL_SCORE = 25;
        private const int SILVER_MEDAL_SCORE = 50;

        public ResultCanvasEmblemImageBuilder()
        {
            position = Vector3.one;
            scale = Vector3.one;
        }

        private Image emblemImage { get; set; }

        private int clearCount { get; set; }

        public ResultCanvasEmblemImageBuilder AddClearCount(int clearCount)
        {
            this.clearCount = clearCount;
            return this;
        }

        public override void Build()
        {
            var size = spriteList.FirstOrDefault()?.rect.size ?? Vector2.zero;
            var image = new GameObject("Emblem");
            emblemImage = image.AddComponent<Image>();
            emblemImage.rectTransform.SetParent(canvas.transform);
            emblemImage.rectTransform.sizeDelta = size;
            emblemImage.rectTransform.localScale = scale;
            emblemImage.rectTransform.anchoredPosition = Vector3.zero;
            var totalWidth = spriteList.FirstOrDefault()?.rect.size.x * scale.x ?? 0f;
            var sx = totalWidth / 2f;
            var sprite = spriteList.FirstOrDefault();
            if (WHITE_MEDAL_SCORE < clearCount && clearCount <= BRONZE_MEDAL_SCORE)
                sprite = spriteList[3];
            else if (BRONZE_MEDAL_SCORE < clearCount && clearCount <= SILVER_MEDAL_SCORE)
                sprite = spriteList[2];
            else if (SILVER_MEDAL_SCORE < clearCount) sprite = spriteList[1];
            emblemImage.sprite = sprite;
            emblemImage.rectTransform.anchoredPosition = new Vector3(sx + position.x, position.y, 0f);
            imageList.Add(emblemImage);
        }

        public override void Reset()
        {
            GameObject.Destroy(emblemImage.gameObject);
            GameObject.Destroy(emblemImage);
        }
    }
}