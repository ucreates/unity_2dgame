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
        var res = validator?.IsValid();
        if (!res?.isSuccess() ?? false)
        {
            notifier?.Notify(NotifyMessage.Title.InputProfileError, res);
            return;
        }

        var input = icanvas.GetComponent<InputCanvasBehaviour>() as IInputUIAsset;
        var paramBody = (Dictionary<string, object>)input.GetInput();
        paramBody.Add("coin", Random.Range(300, 1000));
        ServiceGateway.GetInstance()
            ?.Request("service://player/commit")
            ?.Request(paramBody);
        notifier?.Notify(NotifyMessage.Title.NoticeShow);
    }

    public void OnConfirm()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.InputProfile);
    }

    public void OnPlay()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.GameReady);
    }

    public void OnReplay()
    {
        ServiceGateway.GetInstance()?.Request("service://player/clear")?.Update();
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.GameRestart);
    }

    public void OnRate()
    {
        BaseAssembler<MStoreTable> assembler = new StoreAssembler();
        var tableList = assembler?.WriteToTableList();
        var url = tableList.FirstOrDefault()?.url ?? string.Empty;
        Application.OpenURL(url);
    }

    public void OnRateShow()
    {
        var reviewViewPlugin = PluginFactory.GetPlugin<ReviewViewPlugin>();
        if (RuntimePlatform.IPhonePlayer == Application.platform)
            reviewViewPlugin?.Show("https://itunes.apple.com/jp/app/minecraft/id479516143");
        else if (RuntimePlatform.Android == Application.platform)
            reviewViewPlugin?.Show("market://details?id=com.mojang.minecraftpe");
    }

    public void OnRankingShow()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.RankingShow);
    }

    public void OnRankingHide()
    {
        var notifier = Notifier.GetInstance();
        var previousMessage = notifier.previousMessage;
        notifier?.Notify(NotifyMessage.Title.RankingHide);
        notifier?.Notify(previousMessage.title);
    }

    public void OnNoticeHide()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.NoticeHide);
        notifier?.Notify(NotifyMessage.Title.GameTitle);
    }

    public void OnShopShow()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.ShopShow);
    }

    public void OnShopHide()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.ShopHide);
        notifier?.Notify(NotifyMessage.Title.GameTitle);
    }

    public void OnBuyItemConfirm(int itemId)
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.ShopConfirmShow, itemId);
    }

    public void OnBuyItemCancel()
    {
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.ShopShow);
    }

    public void OnBuyItem()
    {
        var itemId = Session.GetInstance().Get<int>("itemId");
        (int itemId, int amount, string message) parameter = (itemId, 1, string.Empty);
        var response = ServiceGateway.GetInstance()
            ?.Request("service://shop/buy")
            ?.Update(parameter);
        parameter.message = response?.Get<string>("message");
        var notifier = Notifier.GetInstance();
        notifier?.Notify(NotifyMessage.Title.ShopCommitShow, parameter);
    }

    public void OnClickWebSiteButton()
    {
        Application.OpenURL("http://u-creates.com/");
    }
}