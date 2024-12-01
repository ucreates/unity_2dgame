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

namespace Service.Integration.Schema
{
    [Serializable]
    public abstract class BaseFieldSchema
    {
        public virtual bool Equal(object fieldValue)
        {
            return true;
        }

        public virtual bool MoreThan(object fieldValue)
        {
            return true;
        }

        public virtual bool MoreThanEqual(object fieldValue)
        {
            return true;
        }

        public virtual bool LessThan(object fieldValue)
        {
            return true;
        }

        public virtual bool LessThanEqual(object fieldValue)
        {
            return true;
        }
    }
}