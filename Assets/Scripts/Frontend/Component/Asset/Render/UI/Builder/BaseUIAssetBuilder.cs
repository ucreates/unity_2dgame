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
using System;
using System.Collections.Generic;
using Core.Validator.Entity;
using Core.Utility;
namespace Frontend.Component.Asset.Renderer.UI.Builder {
public abstract class BaseUIAssetBuilder {
    private const float INVALID_POSITION_SEED_MIN_VALUE = -10f;
    private const float INVALID_POSITION_SEED_MAX_VALUE = 10f;
    protected bool initialized {
        get;
        set;
    }
    protected float alpha {
        get;
        set;
    }
    public bool enabled {
        get;
        set;
    }
    protected Vector3 position {
        get;
        set;
    }
    protected Vector3 scale {
        get;
        set;
    }
    protected Canvas canvas {
        get;
        set;
    }
    protected List<Sprite> spriteList {
        get;
        set;
    }
    public List<Image> imageList {
        get;
        set;
    }
    public List<InputField> inputFieldList {
        get;
        set;
    }
    public List<Toggle> toggleList {
        get;
        set;
    }
    protected List<Button> buttonList {
        get;
        set;
    }
    protected List<Text> textList {
        get;
        set;
    }
    public BaseUIAssetBuilder() {
        this.initialized = false;
        this.alpha = 0f;
        this.textList = new List<Text>();
        this.spriteList = new List<Sprite>();
        this.buttonList = new List<Button>();
        this.imageList = new List<Image>();
        this.toggleList = new List<Toggle>();
        this.inputFieldList = new List<InputField>();
    }
    public BaseUIAssetBuilder AddCanvas(Canvas canvas) {
        this.canvas = canvas;
        return this;
    }
    public BaseUIAssetBuilder AddAlpha(float alpha) {
        this.alpha = alpha;
        return this;
    }
    public BaseUIAssetBuilder AddButton(Button button) {
        this.buttonList.Add(button);
        return this;
    }
    public BaseUIAssetBuilder AddText(Text text) {
        this.textList.Add(text);
        return this;
    }
    public BaseUIAssetBuilder AddImage(Image image) {
        this.imageList.Add(image);
        return this;
    }
    public BaseUIAssetBuilder AddInputField(InputField inputField) {
        this.inputFieldList.Add(inputField);
        return this;
    }
    public BaseUIAssetBuilder AddToggle(Toggle toggle) {
        this.toggleList.Add(toggle);
        return this;
    }
    public BaseUIAssetBuilder AddSprite(Sprite sprite) {
        this.spriteList.Add(sprite);
        return this;
    }
    public BaseUIAssetBuilder AddSprite(List<Sprite> scoreSpriteList) {
        this.spriteList = scoreSpriteList;
        return this;
    }
    public BaseUIAssetBuilder AddPosition(Vector3 position) {
        this.position = position;
        return this;
    }
    public BaseUIAssetBuilder AddScale(Vector3 scale) {
        this.scale = scale;
        return this;
    }
    public BaseUIAssetBuilder AddEnabled(bool enable) {
        this.enabled = enable;
        return this;
    }
    public BaseUIAssetBuilder AddTransform(Transform transform) {
        Canvas canvas = transform.GetComponent<Canvas>();
        if (null != canvas) {
            this.AddCanvas(canvas);
        }
        Image sprite = transform.GetComponent<Image>();
        if (null != sprite) {
            this.AddImage(sprite);
        }
        Text text = transform.GetComponent<Text>();
        if (null != text) {
            this.AddText(text);
        }
        Toggle toggle = transform.GetComponent<Toggle>();
        if (null != toggle) {
            this.AddToggle(toggle);
        }
        Button button = transform.GetComponent<Button>();
        if (null != button) {
            this.AddButton(button);
        }
        InputField ifield = transform.GetComponent<InputField>();
        if (null != ifield) {
            this.AddInputField(ifield);
        }
        foreach (Transform child in transform) {
            Image csprite = child.GetComponent<Image>();
            if (null != csprite) {
                this.AddImage(csprite);
            }
            Text ctext = child.GetComponent<Text>();
            if (null != ctext) {
                this.AddText(ctext);
            }
            Toggle ctoggle = child.GetComponent<Toggle>();
            if (null != ctoggle) {
                this.AddToggle(ctoggle);
            }
            Button cbutton = child.GetComponent<Button>();
            if (null != cbutton) {
                this.AddButton(cbutton);
            }
            InputField cifield = child.GetComponent<InputField>();
            if (null != cifield) {
                this.AddInputField(cifield);
            }
            if (0 < child.childCount) {
                this.AddTransform(child.transform);
            }
        }
        return this;
    }
    public virtual void Build() {
        return;
    }
    public virtual void Update() {
        return;
    }
    public virtual void Reset() {
        return;
    }
}
}
