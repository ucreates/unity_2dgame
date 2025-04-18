﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Linq;
using Core.Entity;
using Core.Extensions;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Service;
using Service.Integration.Table;
using UnityEngine;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class ConfirmModalDialogShowState : FiniteState<ShopCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private ShopCanvasConfirmModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create(object paramter)
        {
            var allSpriteList = Resources.LoadAll<Sprite>("Textures");
            var itemSpriteList = allSpriteList.Where(sprite => sprite.name.Contains("shop_item_type")).ToList();
            var itemId = paramter.ToInt32();
            Session.GetInstance()?.Add("itemId", itemId);
            var response = ServiceGateway.GetInstance()
                ?.Request("service://shop/confirm")
                ?.Get(itemId);
            var itemmaster = response?.data as MItemTable;
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
            owner.transform.ForEach(child => { child.gameObject.SetActive(child.name.Equals("ConfirmModalDialog")); });
            if (null == builder)
                builder = new ShopCanvasConfirmModalDialogBuilder();
            else
                builder?.Reset();
            var roottrsfrm = owner.transform.Find("ConfirmModalDialog");
            builder
                ?.AddItemSpriteList(itemSpriteList)
                ?.AddItemMaster(itemmaster)
                ?.AddTransform(roottrsfrm)
                ?.AddAlpha(0f)
                ?.AddEnabled(false)
                ?.Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f);
            if (alpha < previousAlpha)
            {
                owner?.stateMachine?.Change("confirmstay");
                return;
            }

            builder
                ?.AddAlpha(alpha)
                ?.Update();
            previousAlpha = alpha;
            alphaTimeLine.Next(1.5f);
        }
    }
}