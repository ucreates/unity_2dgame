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
using Service.Integration.Table;
using UnityEngine;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class ShopCanvasListModalDialogBuilder : BaseUIAssetBuilder
    {
        public ShopCanvasListModalDialogBuilder()
        {
            itemList = new List<string>();
            itemSpriteList = new List<Sprite>();
            itemMasterList = new List<MItemTable>();
        }

        private int coin { get; set; }

        private string commitMessage { get; set; }

        private List<string> itemList { get; set; }

        private List<Sprite> itemSpriteList { get; set; }

        private List<MItemTable> itemMasterList { get; set; }

        public ShopCanvasListModalDialogBuilder AddCoin(int coin)
        {
            this.coin = coin;
            return this;
        }

        public ShopCanvasListModalDialogBuilder AddItemSpriteList(List<Sprite> itemSpriteList)
        {
            this.itemSpriteList = itemSpriteList;
            return this;
        }

        public ShopCanvasListModalDialogBuilder AddItemMasterList(List<MItemTable> itemMasterList)
        {
            this.itemMasterList = itemMasterList;
            return this;
        }

        public ShopCanvasListModalDialogBuilder AddCommitMessage(string commitMessage)
        {
            this.commitMessage = commitMessage;
            return this;
        }

        public ShopCanvasListModalDialogBuilder AddItemList(List<string> itemList)
        {
            this.itemList = itemList;
            return this;
        }

        public override void Build()
        {
            textList.ForEach(text =>
            {
                if (!text.isActiveAndEnabled) return;
                switch (text.name)
                {
                    case "CompleteBuyText":
                        text.text = commitMessage;
                        break;
                    case "RemainingCoinText":
                        text.text = $"coin:{coin}";
                        break;
                    case "ItemTypeAText":
                        text.text = itemMasterList[0].name;
                        break;
                    case "ItemTypeBText":
                        text.text = itemMasterList[1].name;
                        break;
                    case "ItemTypeCText":
                        text.text = itemMasterList[2].name;
                        break;
                    case "ItemTypeDText":
                        text.text = itemMasterList[3].name;
                        break;
                    case "ItemAPriceText":
                        text.text = itemMasterList[0].price.ToString();
                        break;
                    case "ItemBPriceText":
                        text.text = itemMasterList[1].price.ToString();
                        break;
                    case "ItemCPriceText":
                        text.text = itemMasterList[2].price.ToString();
                        break;
                    case "ItemDPriceText":
                        text.text = itemMasterList[3].price.ToString();
                        break;
                }
            });
            Update();
        }

        public override void Update()
        {
            buttonList.ForEach(button =>
            {
                button.FillAlpha(alpha, true, true, true);
                button.enabled = !itemList.Contains(button.name) ? true : false;
            });
            imageList.ForEach(image => { image.FillAlpha(alpha); });
            textList.ForEach(text => { text.FillAlpha(alpha); });
        }
    }
}