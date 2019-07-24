//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Entity;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
namespace Frontend.Behaviour.State.UI.Shop
{
    public sealed class CommitModalDialogStayState : FiniteState<ShopCanvasBehaviour> {
    private ShopCanvasListModalDialogBuilder builder {
        get;
        set;
    }
    public override void Create(Parameter paramter) {
        string message = paramter.Get<string>("message");
        if (null == this.builder) {
            this.builder = new ShopCanvasListModalDialogBuilder();
        } else {
            this.builder.Reset();
        }
        this.builder
        .AddCommitMessage(message)
        .AddTransform(this.owner.transform)
        .AddAlpha(1f)
        .AddEnabled(true)
        .Update();
    }
}
}
