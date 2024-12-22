using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityPlugin.Core.Configure.Sns;

namespace Editor.Build
{
    public class FacebookEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 3;

        protected override void BuildiOS()
        {
            if (!pathDictionary.ContainsKey("fromFrameworkRoot") || !pathDictionary.ContainsKey("destFrameworkRoot")) return;
            var rootDict = plist.root;
            rootDict.SetString("FacebookAppID", FacebookConfigurePlugin.APP_ID);
            rootDict.SetString("FacebookDisplayName", PlayerSettings.productName);
            rootDict.SetString("NSPhotoLibraryUsageDescription", "Photo Access By Unity Facebook Plugin");
        }

        public override void BuildiOSURLSchemes(PlistElementArray bundleURLTypesArray)
        {
            var bundleURLSchemaDict = bundleURLTypesArray.AddDict();
            bundleURLSchemaDict.SetString("CFBundleTypeRole", "Editor");
            var bundleURLSchemaArray = bundleURLSchemaDict.CreateArray("CFBundleURLSchemes");
            bundleURLSchemaArray.AddString("fb" + FacebookConfigurePlugin.APP_ID);
        }

        public override void BuildiOSApplicationQueriesSchemes(PlistElementArray querySchemesArray)
        {
            querySchemesArray.AddString("fbapi");
            querySchemesArray.AddString("fb-messenger-api");
            querySchemesArray.AddString("fbauth2");
            querySchemesArray.AddString("fbshareextension");
        }
    }
}