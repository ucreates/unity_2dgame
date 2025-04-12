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
using Core.Extensions.Array;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Service;
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class ListModalDialogShowState : FiniteState<ShopCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private ShopCanvasListModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            var response = ServiceGateway.GetInstance()
                ?.Request("service://shop/list")
                ?.Get();
            var itemIdList = response?.Get<List<string>>("itemidlist");
            var coin = response?.Get<int>("coin") ?? 0;
            var itemMasterList = response?.Get<List<MItemTable>>("itemmasterlist");
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
            owner.transform.ForEach(child => { child.gameObject.SetActive(child.name.Equals("ListModalDialog")); });
            if (null == builder)
                builder = new ShopCanvasListModalDialogBuilder();
            else
                builder?.Reset();
            var itemTypeList = new string[4] { "A", "B", "C", "D" };
            itemTypeList.ForEach(itemType =>
            {
                var buyButtonTrsfrm = owner.transform.Find($"ListModalDialog/Type{itemType}BuyButton");
                var buyButton = buyButtonTrsfrm.GetComponent<Button>();
                if (itemIdList.Contains(itemType)) buyButton.enabled = false;
            });

            var dialogtrsfrm = owner.transform.Find("ListModalDialog");
            var itemSpriteList = new List<Sprite>();
            var allSpriteList = Resources.LoadAll<Sprite>("Textures");
            allSpriteList.ForEach(sprite =>
            {
                if (sprite.name.Contains("shop_item_type")) itemSpriteList.Add(sprite);
            });

            builder
                ?.AddItemSpriteList(itemSpriteList)
                ?.AddItemMasterList(itemMasterList)
                ?.AddCoin(coin)
                ?.AddTransform(dialogtrsfrm.transform)
                ?.AddAlpha(0f)
                ?.AddEnabled(false)
                ?.Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f);
            if (alpha < previousAlpha)
            {
                owner?.stateMachine?.Change("liststay");
                return;
            }

            builder
                ?.AddAlpha(alpha)
                ?.Update();
            previousAlpha = alpha;
            alphaTimeLine?.Next(1.5f);
        }
    }
}