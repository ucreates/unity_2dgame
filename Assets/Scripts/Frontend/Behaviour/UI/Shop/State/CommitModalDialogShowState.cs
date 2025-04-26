//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Linq;
using Core.Extensions;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class CommitModalDialogShowState : FiniteState<ShopCanvasBehaviour>
    {
        private TimeLine alphaTimeLine { get; set; }

        private ShopCanvasListModalDialogBuilder builder { get; set; }

        private float previousAlpha { get; set; }

        public override void Create(object paramter)
        {
            var sprites = Resources.LoadAll<Sprite>("Textures");
            var itemSpriteList = sprites.Where(sprite => sprite.name.Contains("shop_item_type")).ToList();
            var inputParams = ((int itemId, int amount, string messsage))paramter;
            var canvas = owner.GetComponent<Canvas>();
            if (null != canvas) canvas.enabled = true;
            alphaTimeLine = new TimeLine();
            previousAlpha = 0f;
            owner.transform.ForEach(child => { child.gameObject.SetActive(child.name.Equals("CommitModalDialog")); });
            if (null == builder)
                builder = new ShopCanvasListModalDialogBuilder();
            else
                builder?.Reset();
            builder
                ?.AddItemSpriteList(itemSpriteList)
                ?.AddCommitMessage(inputParams.messsage)
                ?.AddTransform(owner.transform)
                ?.AddAlpha(0f)
                ?.AddEnabled(false)
                ?.Build();
        }

        public override void Update()
        {
            var alpha = Flash.Update(alphaTimeLine?.currentTime ?? 0f);
            if (alpha < previousAlpha)
            {
                owner?.stateMachine?.Change("commitstay", notifyParameter);
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