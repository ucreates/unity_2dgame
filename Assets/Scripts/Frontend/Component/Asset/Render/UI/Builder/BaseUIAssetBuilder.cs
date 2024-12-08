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

        public BaseUIAssetBuilder()
        {
            initialized = false;
            alpha = 0f;
            textList = new List<Text>();
            spriteList = new List<Sprite>();
            buttonList = new List<Button>();
            imageList = new List<Image>();
            toggleList = new List<Toggle>();
            inputFieldList = new List<InputField>();
        }

        protected bool initialized { get; set; }

        protected float alpha { get; set; }

        public bool enabled { get; set; }

        protected Vector3 position { get; set; }

        protected Vector3 scale { get; set; }

        protected Canvas canvas { get; set; }

        protected List<Sprite> spriteList { get; set; }

        public List<Image> imageList { get; set; }

        public List<InputField> inputFieldList { get; set; }

        public List<Toggle> toggleList { get; set; }

        protected List<Button> buttonList { get; set; }

        protected List<Text> textList { get; set; }

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
            var sprite = transform.GetComponent<Image>();
            if (null != sprite) AddImage(sprite);
            var text = transform.GetComponent<Text>();
            if (null != text) AddText(text);
            var toggle = transform.GetComponent<Toggle>();
            if (null != toggle) AddToggle(toggle);
            var button = transform.GetComponent<Button>();
            if (null != button) AddButton(button);
            var ifield = transform.GetComponent<InputField>();
            if (null != ifield) AddInputField(ifield);
            transform.ForEach(child =>
            {
                var csprite = child.GetComponent<Image>();
                if (null != csprite) AddImage(csprite);
                var ctext = child.GetComponent<Text>();
                if (null != ctext) AddText(ctext);
                var ctoggle = child.GetComponent<Toggle>();
                if (null != ctoggle) AddToggle(ctoggle);
                var cbutton = child.GetComponent<Button>();
                if (null != cbutton) AddButton(cbutton);
                var cifield = child.GetComponent<InputField>();
                if (null != cifield) AddInputField(cifield);
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