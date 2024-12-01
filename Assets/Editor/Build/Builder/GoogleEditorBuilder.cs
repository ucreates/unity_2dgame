using System.IO;
using UnityEditor.iOS.Xcode;
using UnityPlugin.Core.Configure.Platform;

namespace Editor.Build
{
    public class GoogleEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 7;

        protected override void BuildiOS()
        {
            if (false == pathDictionary.ContainsKey("fromiOSPluginRoot") ||
                false == pathDictionary.ContainsKey("destiOSProjectRoot")) return;
            var fromiOSRootPath = pathDictionary["fromiOSPluginRoot"];
            var destiOSRootPath = pathDictionary["destiOSProjectRoot"];
            var fromFrameworkRootPath = pathDictionary["fromFrameworkRoot"];
            var destFrameworkRootPath = pathDictionary["destFrameworkRootAbsolute"];
            var innerPlistPathList = new[]
            {
                "GoogleService-Info.plist"
            };
            var innerModuleFilePathList = new[]
            {
                "Google/SignIn/SignIn.h",
                "Google/SignIn/module.modulemap"
            };
            foreach (var innerFilePath in innerPlistPathList)
            {
                var fromPath = Path.Combine(fromiOSRootPath, innerFilePath);
                var destPath = Path.Combine(destiOSRootPath, innerFilePath);
                if (File.Exists(destPath)) File.Delete(destPath);
                File.Copy(fromPath, destPath);
                var bundleGUID = project.AddFile(destPath, innerFilePath);
                project.AddFileToBuild(targetGUID, bundleGUID);
            }

            foreach (var innerFilePath in innerModuleFilePathList)
            {
                var fromPath = Path.Combine(fromFrameworkRootPath, innerFilePath);
                var destPath = Path.Combine(destFrameworkRootPath, innerFilePath);
                if (File.Exists(destPath)) File.Delete(destPath);
                File.Copy(fromPath, destPath);
            }

            project.AddBuildProperty(targetGUID, "SWIFT_INCLUDE_PATHS",
                "$(PROJECT_DIR)/Frameworks/Plugins/iOS/Frameworks/Google/SignIn");
        }

        public override void BuildiOSURLSchemes(PlistElementArray bundleURLTypesArray)
        {
            var bundleURLSchemaDict = bundleURLTypesArray.AddDict();
            bundleURLSchemaDict.SetString("CFBundleTypeRole", "Editor");
            var bundleURLSchemaArray = bundleURLSchemaDict.CreateArray("CFBundleURLSchemes");
            bundleURLSchemaArray.AddString(GoogleConfigurePlugin.REVERSED_CLIENT_ID);
        }
    }
}