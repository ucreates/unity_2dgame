//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Runtime.InteropServices;
using UnityEngine;
using UnityPlugin.Core.Configure;

namespace UnityPlugin.Frontend.Controller
{
    public sealed class PaymentControllerPlugin : BasePlugin
    {
        public const string GOOGLE_PLAY_SKU_TYPE_INAPP = "inapp";
        public const string GOOGLE_PLAY_SKU_TYPE_SUBS = "subs";

        public PaymentControllerPlugin()
        {
            if (RuntimePlatform.Android == Application.platform)
                androidPlugin = new AndroidJavaObject("com.core.scene.TransitionPlugin");
        }

        public override int id => 5;

        [DllImport("__Internal")]
        private static extern void transitionPaymentViewControllerPlugin(string paymentUserId, string paymentProductId);

        public void Payment(string userId, string productId, string androidSKUType = GOOGLE_PLAY_SKU_TYPE_INAPP)
        {
            if (RuntimePlatform.IPhonePlayer == Application.platform)
                transitionPaymentViewControllerPlugin(userId, productId);
            else if (RuntimePlatform.Android == Application.platform)
                if (null != androidPlugin)
                    androidPlugin.CallStatic("transitionPayment", userId, productId, androidSKUType,
                        PaymentConfigurePlugin.PUBLIC_KEY);
        }
    }
}