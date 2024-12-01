//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Core.Validator.Config
{
    public abstract class BaseConfigAssetLoader
    {
        public virtual string Load(string assetName)
        {
            return string.Empty;
        }
    }
}