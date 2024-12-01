//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using UnityEngine;

namespace UnityPlugin
{
    public abstract class BasePlugin
    {
        public virtual int id => 0;

        protected AndroidJavaObject androidPlugin { get; set; }
    }
}