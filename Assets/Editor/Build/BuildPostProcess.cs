using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Editor.Build
{
    public class BuildPostProcess
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            var builderIdList = new[]
            {
                IDEEditorBuilder.BUILDER_ID,
                TwitterEditorBuilder.BUILDER_ID,
                FacebookEditorBuilder.BUILDER_ID,
                LineEditorBuilder.BUILDER_ID,
                WebViewEditorBuilder.BUILDER_ID,
                FirebaseEditorBuilder.BUILDER_ID,
                GoogleEditorBuilder.BUILDER_ID
            };
            if (buildTarget == BuildTarget.iOS)
            {
                var project = new PBXProject();
                var plist = new PlistDocument();
                var projectPath = PBXProject.GetPBXProjectPath(buildPath);
                var plistPath = Path.Combine(buildPath, @"Info.plist");
                var fromCliRootPath = Path.Combine(Environment.CurrentDirectory, @"Assets/Editor/Cli/iOS");
                var destCliRootPath = buildPath;
                var fromFrameworkRootPath =
                    Path.Combine(Environment.CurrentDirectory, @"Assets/Plugins/iOS/Frameworks");
                var destFrameworkRootPath = "Frameworks/Plugins/iOS/Frameworks";
                var destFrameworkRootAbsolutePath = Path.Combine(buildPath, destFrameworkRootPath);
                var fromiOSPluginRootPath = Path.Combine(Environment.CurrentDirectory, @"Assets/Plugins/iOS");
                var destiOSProjectRootPath = buildPath;
                var pathDictionary = new Dictionary<string, string>();
                pathDictionary.Add("build", buildPath);
                pathDictionary.Add("fromCliRoot", fromCliRootPath);
                pathDictionary.Add("destCliRoot", destCliRootPath);
                pathDictionary.Add("fromFrameworkRoot", fromFrameworkRootPath);
                pathDictionary.Add("destFrameworkRoot", destFrameworkRootPath);
                pathDictionary.Add("destFrameworkRootAbsolute", destFrameworkRootAbsolutePath);
                pathDictionary.Add("fromiOSPluginRoot", fromiOSPluginRootPath);
                pathDictionary.Add("destiOSProjectRoot", destiOSProjectRootPath);
                project.ReadFromFile(projectPath);
                plist.ReadFromFile(plistPath);
                var targetGUID = project.GetUnityFrameworkTargetGuid();
                var frameworkList = new[]
                {
                    "StoreKit.framework", "UserNotifications.framework", "SafariServices.framework",
                    "SystemConfiguration.framework", "AddressBook.framework"
                };
                foreach (var framework in frameworkList) project.AddFrameworkToProject(targetGUID, framework, false);
                project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-lz");
                project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
                foreach (var builderId in builderIdList)
                {
                    var builder = EditorBuilderFactory.FactoryMethod(builderId);
                    builder.project = project;
                    builder.plist = plist;
                    builder.targetGUID = targetGUID;
                    builder.pathDictionary = pathDictionary;
                    builder.Build(BuildTarget.iOS);
                    project = builder.project;
                    plist = builder.plist;
                }

                var rootDict = plist.root;
                var bundleURLTypesArray = rootDict.CreateArray("CFBundleURLTypes");
                foreach (var builderId in builderIdList)
                {
                    var builder = EditorBuilderFactory.FactoryMethod(builderId);
                    builder.BuildiOSURLSchemes(bundleURLTypesArray);
                }

                var lsApplicationQueriesSchemesArray = rootDict.CreateArray("LSApplicationQueriesSchemes");
                foreach (var builderId in builderIdList)
                {
                    var builder = EditorBuilderFactory.FactoryMethod(builderId);
                    builder.BuildiOSApplicationQueriesSchemes(lsApplicationQueriesSchemesArray);
                }

                var nsAppTransportSecurityDict = rootDict.CreateDict("NSAppTransportSecurity");
                nsAppTransportSecurityDict.SetBoolean("NSAllowsArbitraryLoads", true);
                var nsExeptionDomainsDict = nsAppTransportSecurityDict.CreateDict("NSExceptionDomains");
                foreach (var builderId in builderIdList)
                {
                    var builder = EditorBuilderFactory.FactoryMethod(builderId);
                    builder.BuildiOSNSAppTransportSecuritySchemes(nsExeptionDomainsDict);
                }

                plist.WriteToFile(plistPath);
                project.WriteToFile(projectPath);
                foreach (var builderId in builderIdList)
                {
                    var builder = EditorBuilderFactory.FactoryMethod(builderId);
                    builder.targetGUID = targetGUID;
                    builder.pathDictionary = pathDictionary;
                    builder.Run(BuildTarget.iOS);
                }
            }
        }
    }
}