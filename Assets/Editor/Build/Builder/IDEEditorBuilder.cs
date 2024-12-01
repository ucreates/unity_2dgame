namespace Editor.Build
{
    public class IDEEditorBuilder : BaseEditorBuilder
    {
        public const int BUILDER_ID = 1;

        protected override void BuildiOS()
        {
            if (null == project) return;
            project.SetBuildProperty(targetGUID, "EMBEDDED_CONTENT_CONTAINS_SWIFT", "YES");
            project.SetBuildProperty(targetGUID, "SWIFT_OBJC_BRIDGING_HEADER",
                "$(SRCROOT)/Libraries/Plugins/iOS/UnityiOSPlugin.h");
            project.SetBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS",
                "$(inherited) @executable_path/Frameworks\"");
            project.SetBuildProperty(targetGUID, "GCC_OPTIMIZATION_LEVEL", "0");
            project.SetBuildProperty(targetGUID, "SWIFT_OPTIMIZATION_LEVEL", "-Onone");
            var rootDict = plist.root;
            rootDict.SetString("NSCameraUsageDescription", "Allow Access Camera By Unity Camera Plugin.");
        }
    }
}