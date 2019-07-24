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
using Core.Entity;
using Core.Validator;
using Core.Validator.Entity;
using Frontend.Behaviour.Base;
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.Property;
using Frontend.Notify;
using Service;
using Service.Integration.Dto.Assembler;
using Service.Integration.Table;
using UnityEngine;
using UnityPlugin;
using UnityPlugin.Frontend.View;
public sealed class EventHandler : BaseBehaviour {
    public void Start() {
        this.property = new BaseProperty(this);
    }
    public void Update() {
        if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    public void OnSubmit() {
        GameObject icanvas = GameObject.Find("InputCanvas");
        if (null == icanvas) {
            return;
        }
        Notifier notifier = Notifier.GetInstance();
        IValidator validator = icanvas.GetComponent<InputCanvasBehaviour>() as IValidator;
        ValidatorResponse res = validator.IsValid();
        if (!res.isSuccess()) {
            Parameter parameter = new Parameter();
            parameter.Set<ValidatorResponse>("ValidateResponse", res);
            notifier.Notify(NotifyMessage.InputProfileError, parameter);
            return;
        }
        IInputUIAsset input = icanvas.GetComponent<InputCanvasBehaviour>() as IInputUIAsset;
        Parameter sparam = input.GetInput();
        sparam.Set<int>("coin", Random.Range(300, 1000));
        ServiceGateway.GetInstance()
        .Request("service://player/commit")
        .Request(sparam);
        notifier.Notify(NotifyMessage.NoticeShow);
    }
    public void OnConfirm() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.InputProfile);
    }
    public void OnPlay() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.GameReady);
    }
    public void OnReplay() {
        ServiceGateway.GetInstance().Request("service://player/clear").Update();
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.GameRestart);
    }
    public void OnRate() {
        BaseAssembler<MStoreTable> assembler = new StoreAssembler();
        List<MStoreTable> tableList = assembler.WriteToTableList();
        string url = tableList[0].url;
        Application.OpenURL(url);
    }
    public void OnRateShow() {
        ReviewViewPlugin reviewViewPlugin = PluginFactory.GetPlugin<ReviewViewPlugin>();
        if (RuntimePlatform.IPhonePlayer == Application.platform) {
            reviewViewPlugin.Show("https://itunes.apple.com/jp/app/minecraft/id479516143");
        } else if (RuntimePlatform.Android == Application.platform) {
            reviewViewPlugin.Show("market://details?id=com.mojang.minecraftpe");
        }
    }
    public void OnRankingShow() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.RankingShow);
    }
    public void OnRankingHide() {
        Notifier notifier = Notifier.GetInstance();
        int previousMessage = notifier.previousMessage;
        notifier.Notify(NotifyMessage.RankingHide);
        notifier.Notify(previousMessage);
    }
    public void OnNoticeHide() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.NoticeHide);
        notifier.Notify(NotifyMessage.GameTitle);
    }
    public void OnShopShow() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopShow);
    }
    public void OnShopHide() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopHide);
        notifier.Notify(NotifyMessage.GameTitle);
    }
    public void OnBuyItemConfirm(int itemId) {
        Parameter nparam = new Parameter();
        nparam.Set<int>("itemId", itemId);
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopConfirmShow, nparam);
    }
    public void OnBuyItemCancel() {
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopShow);
    }
    public void OnBuyItem() {
        int itemId = Session.GetInstance().Get<int>("itemId");
        Parameter parameter = new Parameter();
        parameter.Set<int>("itemId", itemId);
        parameter.Set<int>("amount", 1);
        Response response = ServiceGateway.GetInstance()
                            .Request("service://shop/buy")
                            .Update(parameter);
        parameter = new Parameter();
        parameter.Set<string>("message", response.Get<string>("message"));
        Notifier notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopCommitShow, parameter);
    }
    public void OnClickWebSiteButton() {
        Application.OpenURL("http://u-creates.com/");
    }
}
