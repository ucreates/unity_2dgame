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
using Service.Integration.Table;
using UnityEngine;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class ListModalDialogStayState : FiniteState<ShopCanvasBehaviour>
    {
        private ShopCanvasListModalDialogBuilder builder { get; set; }

        public override void Create()
        {
            builder ??= new ShopCanvasListModalDialogBuilder();
            builder?.Reset();
            var response = ServiceGateway.GetInstance()?.Request("service://shop/list")?.Get();
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            var listModalDialogObject = owner.transform.Find("ListModalDialog");
            var data = ((List<string> itemIdList, List<MItemTable> tableList, int coin))response.data;
            builder
                ?.AddItemList(data.itemIdList)
                ?.AddTransform(listModalDialogObject)
                ?.AddAlpha(1f)
                ?.Update();
        }
    }
}