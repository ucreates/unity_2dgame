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
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Frontend.Component.Asset.Renderer.UI.Builder
{
    public sealed class ShopCanvasConfirmModalDialogBuilder : BaseUIAssetBuilder
    {
        private MItemTable itemMaster { get; set; }

        private List<Sprite> itemSpriteList { get; set; }

        public ShopCanvasConfirmModalDialogBuilder AddItemMaster(MItemTable itemMaster)
        {
            this.itemMaster = itemMaster;
            return this;
        }

        public ShopCanvasConfirmModalDialogBuilder AddItemSpriteList(List<Sprite> itemSpriteList)
        {
            this.itemSpriteList = itemSpriteList;
            return this;
        }

        public override void Build()
        {
            textList.ForEach(text =>
            {
                if (text.name.Equals("ItemTypeText")) text.text = itemMaster.name;
                else if (text.name.Equals("ItemPriceText")) text.text = itemMaster.price.ToString();
            });
            var suffix = itemMaster.name.Substring(itemMaster.name.Length - 1, 1).ToLower();
            var imageCallback = new Func<Image, bool>(delegate(Image image) { return image.name.Equals("ItemThumbnailImage"); });
            var spriteCallback = new Func<Sprite, bool>(delegate(Sprite sprite) { return sprite.name.ToLower().Contains(suffix); });
            if (imageList.Any(imageCallback) && itemSpriteList.Any(spriteCallback))
            {
                var image = imageList?.FirstOrDefault(imageCallback) ?? null;
                image.sprite = itemSpriteList?.FirstOrDefault(spriteCallback) ?? null;
            }
        }

        public override void Update()
        {
            buttonList.ForEach(button =>
            {
                var cb = button.colors;
                cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, alpha);
                cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, alpha);
                cb.highlightedColor = new Color(cb.highlightedColor.r, cb.highlightedColor.g, cb.highlightedColor.b,
                    alpha);
                button.colors = cb;
            });
            imageList.ForEach(image => { image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); });
            textList.ForEach(text => { text.color = new Color(text.color.r, text.color.g, text.color.b, alpha); });
        }
    }
}