//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Core.Validator;
using Frontend.Component.Asset.Renderer.UI;
using Frontend.Component.Property;
using Frontend.Notify;
using Service;
using Service.Integration.Dto.Assembler;
using Service.Integration.Table;
using UnityEngine;
using UnityPlugin;
using UnityPlugin.Frontend.View;

public sealed class EventHandler : BaseBehaviour
{
    public void Start()
    {
        property = new BaseProperty(this);
    }

    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape)) Application.Quit();
    }

    public void OnSubmit()
    {
        var icanvas = GameObject.Find("InputCanvas");
        if (null == icanvas) return;
        var notifier = Notifier.GetInstance();
        var validator = icanvas.GetComponent<InputCanvasBehaviour>() as IValidator;
        var res = validator.IsValid();
        if (!res.isSuccess())
        {
            var parameter = new Parameter();
            parameter.Set("ValidateResponse", res);
            notifier.Notify(NotifyMessage.InputProfileError, parameter);
            return;
        }

        var input = icanvas.GetComponent<InputCanvasBehaviour>() as IInputUIAsset;
        var sparam = input.GetInput();
        sparam.Set("coin", Random.Range(300, 1000));
        ServiceGateway.GetInstance()
            .Request("service://player/commit")
            .Request(sparam);
        notifier.Notify(NotifyMessage.NoticeShow);
    }

    public void OnConfirm()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.InputProfile);
    }

    public void OnPlay()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.GameReady);
    }

    public void OnReplay()
    {
        ServiceGateway.GetInstance().Request("service://player/clear").Update();
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.GameRestart);
    }

    public void OnRate()
    {
        BaseAssembler<MStoreTable> assembler = new StoreAssembler();
        var tableList = assembler.WriteToTableList();
        var url = tableList[0].url;
        Application.OpenURL(url);
    }

    public void OnRateShow()
    {
        var reviewViewPlugin = PluginFactory.GetPlugin<ReviewViewPlugin>();
        if (RuntimePlatform.IPhonePlayer == Application.platform)
            reviewViewPlugin.Show("https://itunes.apple.com/jp/app/minecraft/id479516143");
        else if (RuntimePlatform.Android == Application.platform)
            reviewViewPlugin.Show("market://details?id=com.mojang.minecraftpe");
    }

    public void OnRankingShow()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.RankingShow);
    }

    public void OnRankingHide()
    {
        var notifier = Notifier.GetInstance();
        var previousMessage = notifier.previousMessage;
        notifier.Notify(NotifyMessage.RankingHide);
        notifier.Notify(previousMessage);
    }

    public void OnNoticeHide()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.NoticeHide);
        notifier.Notify(NotifyMessage.GameTitle);
    }

    public void OnShopShow()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopShow);
    }

    public void OnShopHide()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopHide);
        notifier.Notify(NotifyMessage.GameTitle);
    }

    public void OnBuyItemConfirm(int itemId)
    {
        var nparam = new Parameter();
        nparam.Set("itemId", itemId);
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopConfirmShow, nparam);
    }

    public void OnBuyItemCancel()
    {
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopShow);
    }

    public void OnBuyItem()
    {
        var itemId = Session.GetInstance().Get<int>("itemId");
        var parameter = new Parameter();
        parameter.Set("itemId", itemId);
        parameter.Set("amount", 1);
        var response = ServiceGateway.GetInstance()
            .Request("service://shop/buy")
            .Update(parameter);
        parameter = new Parameter();
        parameter.Set("message", response.Get<string>("message"));
        var notifier = Notifier.GetInstance();
        notifier.Notify(NotifyMessage.ShopCommitShow, parameter);
    }

    public void OnClickWebSiteButton()
    {
        Application.OpenURL("http://u-creates.com/");
    }
}