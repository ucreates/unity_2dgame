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
using Core.Entity;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Notify;
using Service;
using Service.Strategy;
using Service.Integration;
using Service.Integration.Table;
namespace Frontend.Behaviour.State.UI.Shop {
public sealed class ListModalDialogShowState : FiniteState<ShopCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private ShopCanvasListModalDialogBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    public override void Create() {
        Response response =  ServiceGateway.GetInstance()
                             .Request("service://shop/list")
                             .Get();
        List<string> itemIdList = response.Get<List<string>>("itemidlist");
        int coin = response.Get<int>("coin");
        List<MItemTable> itemMasterList = response.Get<List<MItemTable>>("itemmasterlist");
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.alphaTimeLine = new TimeLine();
        this.previousAlpha = 0f;
        foreach (Transform child in this.owner.transform) {
            if (child.name.Equals("ListModalDialog")) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
        if (null == this.builder) {
            this.builder = new ShopCanvasListModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        string[] itemTypeList = new string[4] {"A", "B", "C", "D"};
        for (int i = 0; i < itemTypeList.Length; i++) {
            string type = itemTypeList[i];
            Transform buyButtonTrsfrm = this.owner.transform.FindChild("ListModalDialog/Type" + type + "BuyButton");
            Button buyButton = buyButtonTrsfrm.GetComponent<Button>();
            if (itemIdList.Contains(type)) {
                buyButton.enabled = false;
            }
        }
        Transform dialogtrsfrm = this.owner.transform.FindChild("ListModalDialog");
        Sprite[] allSpriteList = Resources.LoadAll<Sprite>("Sprite");
        List<Sprite> itemSpriteList = new List<Sprite>();
        for (int i = 0; i < allSpriteList.Length; i++) {
            Sprite sprite = allSpriteList[i];
            if (sprite.name.Contains("shop_item_type")) {
                itemSpriteList.Add(sprite);
            }
        }
        this.builder
        .AddItemSpriteList(itemSpriteList)
        .AddItemMasterList(itemMasterList)
        .AddCoin(coin)
        .AddTransform(dialogtrsfrm.transform)
        .AddAlpha(0f)
        .AddEnabled(false)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("liststay");
            return;
        }
        this.builder
        .AddAlpha(alpha)
        .Update();
        this.previousAlpha = alpha;
        this.alphaTimeLine.Next(1.5f);
    }
}
}
