//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Frontend.Component.Asset.Render
{
    public class BaseRenderAsset : BaseAsset
    {
        public BaseRenderAsset()
        {
        }

        public BaseRenderAsset(BaseBehaviour owner)
        {
            this.owner = owner;
        }

        public BaseBehaviour owner { get; protected set; }

        public virtual void Show()
        {
        }

        public virtual void Hide()
        {
        }
    }
}