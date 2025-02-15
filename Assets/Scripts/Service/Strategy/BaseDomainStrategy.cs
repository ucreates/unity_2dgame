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

namespace Service.Strategy
{
    public class BaseStrategy
    {
        public virtual Response Get(object parameter = null)
        {
            return null;
        }

        public virtual Response Load(object parameter = null)
        {
            return null;
        }

        public virtual Response Request(object parameter = null)
        {
            return null;
        }

        public virtual Response Update(object parameter = null)
        {
            return null;
        }

        public virtual Response Clear()
        {
            return null;
        }
    }
}