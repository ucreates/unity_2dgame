//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<string> itemList { get; set; } = new();

        private List<Sprite> itemSpriteList { get; set; } = new();

        private List<MItemTable> itemMasterList { get; set; } = new();

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
            Func<string, int> getIndex = category => category switch
            {
                string when category.IsNullOrEmpty() => -1,
                string when category.ToLower().Contains("typea") => 0,
                string when category.ToLower().Contains("typeb") => 1,
                string when category.ToLower().Contains("typec") => 2,
                string when category.ToLower().Contains("typed") => 3
            };
            textList.ForEach(text =>
            {
                if (!text.isActiveAndEnabled) return;
                var index = -1;
                switch (text.name)
                {
                    case "CompleteBuyText":
                        text.text = commitMessage;
                        break;
                    case "RemainingCoinText":
                        text.text = $"coin:{coin}";
                        break;
                    case string name when name.ToLower().Contains("name"):
                        index = getIndex(name);
                        text.text = itemMasterList[index].name;
                        break;
                    case string name when name.ToLower().Contains("price"):
                        index = getIndex(name);
                        text.text = itemMasterList[index].price.ToString();
                        break;
                }
            });
            imageList.Where(image => image.name.ToLower().Contains("itemtype")).ForEach(image =>
            {
                switch (image.name)
                {
                    case string name when name.ToLower().Contains("item"):
                        var sprite = itemSpriteList[getIndex(name)];
                        image.sprite = sprite;
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