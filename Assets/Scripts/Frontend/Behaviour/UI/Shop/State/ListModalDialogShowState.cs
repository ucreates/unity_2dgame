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
        private TimeLine alphaTimeLine { get; } = new();

        private ShopCanvasListModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create()
        {
            var response = ServiceGateway.GetInstance()
                ?.Request("service://shop/list")
                ?.Get();
            var data = ((List<string> itemIdList, List<MItemTable> itemMasterList, int coin))response?.data;
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            previousAlpha = 0f;
            owner.transform.ForEach(child => { child.gameObject.SetActive(child.name.Equals("ListModalDialog")); });
            builder ??= new ShopCanvasListModalDialogBuilder();
            builder?.Reset();
            var itemCategories = new string[4] { "A", "B", "C", "D" };
            itemCategories.ForEach(itemType =>
            {
                var buyButtonObject = owner.transform.Find($"ListModalDialog/Type{itemType}BuyButton");
                var buyButton = buyButtonObject.GetComponent<Button>();
                if (data.itemIdList.Contains(itemType)) buyButton.enabled = false;
            });

            var listModalDialogObject = owner.transform.Find("ListModalDialog");
            var sprites = Resources.LoadAll<Sprite>("Textures");
            var itemSpriteList = sprites.Where(sprite => sprite.name.Contains("shop_item_type")).ToList();
            builder
                ?.AddItemSpriteList(itemSpriteList)
                ?.AddItemMasterList(data.itemMasterList)
                ?.AddCoin(data.coin)
                ?.AddTransform(listModalDialogObject.transform)
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