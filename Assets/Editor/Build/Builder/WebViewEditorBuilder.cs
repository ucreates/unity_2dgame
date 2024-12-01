//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using UnityEditor.iOS.Xcode;
using UnityPlugin.Core.Configure;

namespace Editor.Build
{
    public class WebViewEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 5;

        public override void BuildiOSNSAppTransportSecuritySchemes(PlistElementDict nsExeptionDomainsDict)
        {
            for (var i = 0; i < WebViewConfigurePlugin.ALLOW_DOMAIN_LIST.Length; i++)
            {
                var domain = WebViewConfigurePlugin.ALLOW_DOMAIN_LIST[i];
                var domainDict = nsExeptionDomainsDict.CreateDict(domain);
                domainDict.SetBoolean("NSTemporaryExceptionAllowsInsecureHTTPLoads", true);
            }
        }
    }
}