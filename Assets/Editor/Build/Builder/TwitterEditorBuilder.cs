using System.Diagnostics;
using System.IO;
using UnityPlugin.Core.Configure.Sns;
using Debug = UnityEngine.Debug;

namespace Editor.Build
{
    public class TwitterEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 2;

        protected override string runScriptName => "fabric_run_script.rb";

        protected override void BuildiOS()
        {
            if (false == pathDictionary.ContainsKey("fromFrameworkRoot") ||
                false == pathDictionary.ContainsKey("destFrameworkRoot")) return;
            var fromFrameworkRootPath = pathDictionary["fromFrameworkRoot"];
            var destFrameworkRootPath = pathDictionary["destFrameworkRoot"];
            var innerBundlePathList = new[]
            {
                "Twitter/TwitterKit.framework/TwitterKitResources.bundle"
            };
            foreach (var innerBundlePath in innerBundlePathList)
            {
                var fromPath = Path.Combine(fromFrameworkRootPath, innerBundlePath);
                var destPath = Path.Combine(destFrameworkRootPath, innerBundlePath);
                var bundleGUID = project.AddFile(fromPath, destPath);
                project.AddFileToBuild(targetGUID, bundleGUID);
            }

            var rootDict = plist.root;
            var fabricDict = rootDict.CreateDict("Fabric");
            fabricDict.SetString("APIKey", TwitterConfigurePlugin.API_KEY);
            var kitsArray = fabricDict.CreateArray("Kits");
            var kitsDict = kitsArray.AddDict();
            var keyInfoDict = kitsDict.CreateDict("KitInfo");
            keyInfoDict.SetString("consumerKey", TwitterConfigurePlugin.CONSUMER_KEY);
            keyInfoDict.SetString("consumerSecret", TwitterConfigurePlugin.CONSUMER_SEACRET);
            kitsDict.SetString("KitName", "Twitter");
        }

        protected override void RuniOS()
        {
            var buildPath = pathDictionary["build"];
            var projectPath = Path.Combine(buildPath, "Unity-iPhone.xcodeproj");
            var fromCliRootPath = pathDictionary["fromCliRoot"];
            var destCliRootPath = pathDictionary["destCliRoot"];
            var fromCliPath = Path.Combine(fromCliRootPath, runScriptName);
            var destCliPath = Path.Combine(destCliRootPath, runScriptName);
            if (File.Exists(destCliPath)) File.Delete(destCliPath);
            File.Copy(fromCliPath, destCliPath);
            var builder = new CommandEditorBuilder();
            builder.commandElementList.Add(destCliPath);
            builder.commandElementList.Add(projectPath);
            builder.commandElementList.Add("./Frameworks/Plugins/iOS/Frameworks/Twitter/Fabric.framework/run");
            builder.commandElementList.Add(TwitterConfigurePlugin.API_KEY);
            builder.commandElementList.Add(TwitterConfigurePlugin.BUILD_SEACRET);
            var info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.FileName = "/usr/bin/ruby";
            info.Arguments = builder.Build();
            info.WorkingDirectory = buildPath;
            info.CreateNoWindow = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            var process = new Process();
            process.StartInfo = info;
            var ret = process.Start();
            if (false == ret)
            {
                Debug.LogError("faild process start.");
                return;
            }

            var stdout = process.StandardOutput.ReadToEnd();
            var stderror = process.StandardError.ReadToEnd();
            Debug.Log(stdout);
            Debug.Log(stderror);
            process.WaitForExit();
            process.Close();
        }
    }
}