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
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public abstract class BaseUIAssetBuilder
    {
        private const float INVALID_POSITION_SEED_MIN_VALUE = -10f;
        private const float INVALID_POSITION_SEED_MAX_VALUE = 10f;

        protected bool initialized { get; set; } = false;

        protected float alpha { get; set; }

        public bool enabled { get; set; } = true;

        protected Vector3 position { get; set; } = Vector3.zero;

        protected Vector3 scale { get; set; } = Vector3.zero;

        protected Canvas canvas { get; set; }

        protected List<Sprite> spriteList { get; set; } = new();

        public List<Image> imageList { get; set; } = new();

        public List<InputField> inputFieldList { get; set; } = new();

        public List<Toggle> toggleList { get; set; } = new();

        protected List<Button> buttonList { get; set; } = new();

        protected List<Text> textList { get; set; } = new();

        public BaseUIAssetBuilder AddCanvas(Canvas canvas)
        {
            this.canvas = canvas;
            return this;
        }

        public BaseUIAssetBuilder AddAlpha(float alpha)
        {
            this.alpha = alpha;
            return this;
        }

        public BaseUIAssetBuilder AddButton(Button button)
        {
            buttonList.Add(button);
            return this;
        }

        public BaseUIAssetBuilder AddText(Text text)
        {
            textList.Add(text);
            return this;
        }

        public BaseUIAssetBuilder AddImage(Image image)
        {
            imageList.Add(image);
            return this;
        }

        public BaseUIAssetBuilder AddInputField(InputField inputField)
        {
            inputFieldList.Add(inputField);
            return this;
        }

        public BaseUIAssetBuilder AddToggle(Toggle toggle)
        {
            toggleList.Add(toggle);
            return this;
        }

        public BaseUIAssetBuilder AddSprite(Sprite sprite)
        {
            spriteList.Add(sprite);
            return this;
        }

        public BaseUIAssetBuilder AddSprite(List<Sprite> scoreSpriteList)
        {
            spriteList = scoreSpriteList;
            return this;
        }

        public BaseUIAssetBuilder AddPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        public BaseUIAssetBuilder AddScale(Vector3 scale)
        {
            this.scale = scale;
            return this;
        }

        public BaseUIAssetBuilder AddEnabled(bool enable)
        {
            enabled = enable;
            return this;
        }

        public BaseUIAssetBuilder AddTransform(Transform transform)
        {
            var canvas = transform.GetComponent<Canvas>();
            if (null != canvas) AddCanvas(canvas);
            var image = transform.GetComponent<Image>();
            if (null != image) AddImage(image);
            var text = transform.GetComponent<Text>();
            if (null != text) AddText(text);
            var toggle = transform.GetComponent<Toggle>();
            if (null != toggle) AddToggle(toggle);
            var button = transform.GetComponent<Button>();
            if (null != button) AddButton(button);
            var inputField = transform.GetComponent<InputField>();
            if (null != inputField) AddInputField(inputField);
            transform.ForEach(child =>
            {
                var childSprite = child.GetComponent<Image>();
                if (null != childSprite) AddImage(childSprite);
                var childText = child.GetComponent<Text>();
                if (null != childText) AddText(childText);
                var childToggle = child.GetComponent<Toggle>();
                if (null != childToggle) AddToggle(childToggle);
                var childButton = child.GetComponent<Button>();
                if (null != childButton) AddButton(childButton);
                var childInputField = child.GetComponent<InputField>();
                if (null != childInputField) AddInputField(childInputField);
                if (0 < child.childCount) AddTransform(child.transform);
            });
            return this;
        }

        public virtual void Build()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Reset()
        {
        }
    }
}