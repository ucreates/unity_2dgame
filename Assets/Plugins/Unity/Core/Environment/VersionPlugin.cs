//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityPlugin.Core.Environment
{
    public sealed class VersionPlugin : BasePlugin
    {
        public VersionPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.environment.VersionPlugin");
        }

        [DllImport("__Internal")]
        private static extern int getVersionPlugin();

        [DllImport("__Internal")]
        private static extern string getVersionNamePlugin();

        public int GetVersion()
        {
            var version = 0;
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                version = getVersionPlugin();
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                version = androidPlugin.CallStatic<int>("getVersion");
            }
            else
            {
                var fVersion = Convert.ToSingle(Application.version);
                version = Convert.ToInt32(fVersion);
            }

            return version;
        }

        public string GetVersionName()
        {
            var versionName = "0.0.0";
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                versionName = getVersionNamePlugin();
            else if (RuntimePlatform.Android == Application.platform)
                versionName = androidPlugin.CallStatic<string>("getVersionName");
            else
                versionName = Application.version;
            return versionName;
        }
    }
}