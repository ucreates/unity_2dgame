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
using Service.Integration;
using Service.Integration.Table;
namespace Frontend.Component.Asset.Renderer.UI.Builder {
public sealed class ShopCanvasListModalDialogBuilder : BaseUIAssetBuilder {
    private int coin {
        get;
        set;
    }
    private string commitMessage {
        get;
        set;
    }
    private List<string> itemList {
        get;
        set;
    }
    private List<Sprite> itemSpriteList {
        get;
        set;
    }
    private List<MItemTable> itemMasterList {
        get;
        set;
    }
    public ShopCanvasListModalDialogBuilder() {
        this.itemList = new List<string>();
        this.itemSpriteList = new List<Sprite>();
        this.itemMasterList = new List<MItemTable>();
    }
    public ShopCanvasListModalDialogBuilder AddCoin(int coin) {
        this.coin = coin;
        return this;
    }
    public ShopCanvasListModalDialogBuilder AddItemSpriteList(List<Sprite> itemSpriteList) {
        this.itemSpriteList = itemSpriteList;
        return this;
    }
    public ShopCanvasListModalDialogBuilder AddItemMasterList(List<MItemTable> itemMasterList) {
        this.itemMasterList = itemMasterList;
        return this;
    }
    public ShopCanvasListModalDialogBuilder AddCommitMessage(string commitMessage) {
        this.commitMessage = commitMessage;
        return this;
    }
    public ShopCanvasListModalDialogBuilder AddItemList(List<string> itemList) {
        this.itemList = itemList;
        return this;
    }
    public override void Build() {
        foreach (Text text in this. textList) {
            if (!text.isActiveAndEnabled) {
                continue;
            }
            if (text.name.Equals("CompleteBuyText")) {
                text.text = this.commitMessage;
            } else if (text.name.Equals("RemainingCoinText")) {
                text.text = "coin:" + this.coin.ToString();
            } else if (text.name.Equals("ItemTypeAText")) {
                text.text = this.itemMasterList[0].name;
            } else if (text.name.Equals("ItemTypeBText")) {
                text.text = this.itemMasterList[1].name;
            } else if (text.name.Equals("ItemTypeCText")) {
                text.text = this.itemMasterList[2].name;
            } else if (text.name.Equals("ItemTypeDText")) {
                text.text = this.itemMasterList[3].name;
            } else if (text.name.Equals("ItemAPriceText")) {
                text.text = this.itemMasterList[0].price.ToString();
            } else if (text.name.Equals("ItemBPriceText")) {
                text.text = this.itemMasterList[1].price.ToString();
            } else if (text.name.Equals("ItemCPriceText")) {
                text.text = this.itemMasterList[2].price.ToString();
            } else if (text.name.Equals("ItemDPriceText")) {
                text.text = this.itemMasterList[3].price.ToString();
            }
        }
        this.Update();
    }
    public override void Update() {
        foreach (Button button in this.buttonList) {
            ColorBlock cb = button.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, this.alpha);
            cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, this.alpha);
            cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b, this.alpha);
            button.colors = cb;
            if (!this.itemList.Contains(button.name)) {
                button.enabled =  true;
            } else {
                button.enabled = false;
            }
        }
        foreach (Image image in this.imageList) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, this.alpha);
        }
        foreach (Text text in this. textList) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, this.alpha);
        }
    }
    public override void Reset() {
    }
}
}
