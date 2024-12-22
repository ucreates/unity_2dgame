using System.IO;

namespace Editor.Build
{
    public class FirebaseEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 6;

        protected override void BuildiOS()
        {
            if (!pathDictionary.ContainsKey("fromFrameworkRoot") || !pathDictionary.ContainsKey("destFrameworkRoot")) return;
            var fromFrameworkRootPath = pathDictionary["fromFrameworkRoot"];
            var destFrameworkRootPath = pathDictionary["destFrameworkRootAbsolute"];
            var innerFilePathList = new[]
            {
                "Firebase/Firebase.h",
                "Firebase/module.modulemap"
            };
            foreach (var innerFilePath in innerFilePathList)
            {
                var fromPath = Path.Combine(fromFrameworkRootPath, innerFilePath);
                var destPath = Path.Combine(destFrameworkRootPath, innerFilePath);
                if (File.Exists(destPath)) File.Delete(destPath);
                File.Copy(fromPath, destPath);
            }

            project.AddBuildProperty(targetGUID, "SWIFT_INCLUDE_PATHS",
                "$(PROJECT_DIR)/Frameworks/Plugins/iOS/Frameworks/Firebase");
        }
    }
}