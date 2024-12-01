using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityPlugin.Core.Configure.Sns;

namespace Editor.Build
{
    public class LineEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 4;

        protected override void BuildiOS()
        {
            if (false == pathDictionary.ContainsKey("fromFrameworkRoot") ||
                false == pathDictionary.ContainsKey("destFrameworkRoot")) return;
            var rootDict = plist.root;
            var frameworks = new[] { "CoreText.framework", "CoreTelephony.framework", "Security.framework" };
            foreach (var framework in frameworks) project.AddFrameworkToProject(targetGUID, framework, false);
            var lineAdapterConfigDict = rootDict.CreateDict("LineAdapterConfig");
            lineAdapterConfigDict.SetString("ChannelId", LineConfigurePlugin.CHANNEL_ID);
        }

        public override void BuildiOSURLSchemes(PlistElementArray bundleURLTypesArray)
        {
            var bundleURLSchemaDict = bundleURLTypesArray.AddDict();
            bundleURLSchemaDict.SetString("CFBundleTypeRole", "Editor");
            bundleURLSchemaDict.SetString("CFBundleURLName", PlayerSettings.applicationIdentifier);
            var bundleURLSchemaArray = bundleURLSchemaDict.CreateArray("CFBundleURLSchemes");
            bundleURLSchemaArray.AddString("line3rdp." + PlayerSettings.applicationIdentifier);
        }

        public override void BuildiOSApplicationQueriesSchemes(PlistElementArray querySchemesArray)
        {
            querySchemesArray.AddString("lineauth");
            querySchemesArray.AddString("line3rdp." + PlayerSettings.applicationIdentifier);
        }

        public override void BuildiOSNSAppTransportSecuritySchemes(PlistElementDict nsExeptionDomainsDict)
        {
            var obsLineAppsComDict = nsExeptionDomainsDict.CreateDict("obs.line-apps.com");
            obsLineAppsComDict.SetBoolean("NSIncludesSubdomains", true);
            obsLineAppsComDict.SetBoolean("NSThirdPartyExceptionAllowsInsecureHTTPLoads", true);
            obsLineAppsComDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var dlProfileLineCdnDict = nsExeptionDomainsDict.CreateDict("dl.profile.line-cdn.net");
            dlProfileLineCdnDict.SetBoolean("NSIncludesSubdomains", true);
            dlProfileLineCdnDict.SetBoolean("NSThirdPartyExceptionAllowsInsecureHTTPLoads", true);
            dlProfileLineCdnDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var dlProfileLineNaverDict = nsExeptionDomainsDict.CreateDict("dl.profile.line.naver.jp");
            dlProfileLineNaverDict.SetBoolean("NSIncludesSubdomains", true);
            dlProfileLineNaverDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var icsNaverDict = nsExeptionDomainsDict.CreateDict("lcs.naver.jp");
            icsNaverDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var scdnLineAppsDict = nsExeptionDomainsDict.CreateDict("scdn.line-apps.com");
            scdnLineAppsDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var accessLineMeDict = nsExeptionDomainsDict.CreateDict("access.line.me");
            accessLineMeDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
            var appLineMeDict = nsExeptionDomainsDict.CreateDict("app.line.me");
            appLineMeDict.SetBoolean("NSThirdPartyExceptionRequiresForwardSecrecy", false);
        }
    }
}