using System.Collections.Generic;
using UnityEditor;
using UnityEditor.iOS.Xcode;

namespace Editor.Build
{
    public abstract class BaseEditorBuilder
    {
        public BaseEditorBuilder()
        {
            project = null;
            plist = null;
            targetGUID = string.Empty;
            pathDictionary = new Dictionary<string, string>();
        }

        public PBXProject project { get; set; }

        public PlistDocument plist { get; set; }

        public string targetGUID { protected get; set; }

        public Dictionary<string, string> pathDictionary { protected get; set; }

        protected virtual string runScriptName => string.Empty;

        public void Build(BuildTarget target)
        {
            if (target == BuildTarget.iOS)
                BuildiOS();
            else if (target == BuildTarget.Android) BuildAndroid();
        }

        protected virtual void BuildiOS()
        {
        }

        protected virtual void BuildAndroid()
        {
        }

        public void Run(BuildTarget target)
        {
            if (target == BuildTarget.iOS)
                RuniOS();
            else if (target == BuildTarget.Android) RunAndroid();
        }

        protected virtual void RuniOS()
        {
        }

        protected virtual void RunAndroid()
        {
        }

        public virtual void BuildiOSURLSchemes(PlistElementArray bundleURLSchemaDict)
        {
        }

        public virtual void BuildiOSApplicationQueriesSchemes(PlistElementArray querySchemesArray)
        {
        }

        public virtual void BuildiOSNSAppTransportSecuritySchemes(PlistElementDict nsExeptionDomainsDict)
        {
        }
    }
}