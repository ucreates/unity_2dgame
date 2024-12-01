//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Runtime.InteropServices;
using UnityEngine;
using UnityPlugin.Core.Configure.Platform;

namespace UnityPlugin.Frontend.Controller.Platform
{
    public sealed class GoogleControllerPlugin : BasePlugin
    {
        private const int LOGIN = 0;
        private const int LOGOUT = 1;
        private const int REVOKE_ACCESS = 2;

        public GoogleControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        [DllImport("__Internal")]
        private static extern void transitionGoogleViewControllerPlugin(string clientId, int mode);

        public void LogIn()
        {
            Transition(LOGIN);
        }

        public void LogOut()
        {
            Transition(LOGOUT);
        }

        public void RevokeAccess()
        {
            Transition(REVOKE_ACCESS);
        }

        private void Transition(int authorizeType)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
            {
                transitionGoogleViewControllerPlugin(GoogleConfigurePlugin.iOS_CLIENT_ID, authorizeType);
            }
            else if (RuntimePlatform.Android == Application.platform)
            {
                if (null == androidPlugin) return;
                androidPlugin.CallStatic("transitionGoogle", GoogleConfigurePlugin.ANDROID_CLIENT_ID, authorizeType);
            }
        }
    }
}