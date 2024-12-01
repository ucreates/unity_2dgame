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
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Service;
using UnityEngine;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class ListModalDialogStayState : FiniteState<ShopCanvasBehaviour>
    {
        private ShopCanvasListModalDialogBuilder builder { get; set; }

        public override void Create()
        {
            if (null == builder)
                builder = new ShopCanvasListModalDialogBuilder();
            else
                builder.Reset();
            var response = ServiceGateway.GetInstance().Request("service://shop/list").Get();
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            var trsfrm = owner.transform.Find("ListModalDialog");
            var itemIdList = response.Get<List<string>>("itemidlist");
            builder
                .AddItemList(itemIdList)
                .AddTransform(trsfrm)
                .AddAlpha(1f)
                .Update();
        }
    }
}