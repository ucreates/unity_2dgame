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
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Notify;
using Service;
using Service.Strategy;
using Service.Integration;
using Service.Integration.Schema;
using Service.Integration.Table;
namespace Frontend.Behaviour.State.UI.Shop {
public sealed class ConfirmModalDialogShowState : FiniteState<ShopCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private ShopCanvasConfirmModalDialogBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    private Parameter notifyParameter {
        get;
        set;
    }
    public override void Create(Parameter paramter) {
        Sprite[] allSpriteList = Resources.LoadAll<Sprite>("Sprite");
        List<Sprite> itemSpriteList = new List<Sprite>();
        for (int i = 0; i < allSpriteList.Length; i++) {
            Sprite sprite = allSpriteList[i];
            if (sprite.name.Contains("shop_item_type")) {
                itemSpriteList.Add(sprite);
            }
        }
        int itemId = paramter.Get<int>("itemId");
        Session.GetInstance().Add("itemId", itemId);
        Parameter sparam = new Parameter();
        sparam.Set<int>("itemId", itemId);
        Response response = ServiceGateway.GetInstance()
                            .Request("service://shop/confirm")
                            .Get(sparam);
        MItemTable itemmaster = response.Get<MItemTable>("itemmaster");
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.alphaTimeLine = new TimeLine();
        this.previousAlpha = 0f;
        foreach (Transform child in this.owner.transform) {
            if (child.name.Equals("ConfirmModalDialog")) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
        if (null == this.builder) {
            this.builder = new ShopCanvasConfirmModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        Transform roottrsfrm = this.owner.transform.FindChild("ConfirmModalDialog");
        this.builder
        .AddItemSpriteList(itemSpriteList)
        .AddItemMaster(itemmaster)
        .AddTransform(roottrsfrm)
        .AddAlpha(0f)
        .AddEnabled(false)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("confirmstay");
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
