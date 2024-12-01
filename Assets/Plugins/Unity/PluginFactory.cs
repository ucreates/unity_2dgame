//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;

namespace UnityPlugin
{
    public class PluginFactory
    {
        public static T GetPlugin<T>() where T : BasePlugin, new()
        {
            var type = typeof(T);
            return Activator.CreateInstance(type) as T;
        }
    }
}