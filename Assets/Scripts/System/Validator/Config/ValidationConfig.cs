//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using System.Xml;

namespace Core.Validator.Config
{
    public sealed class ValidationConfig
    {
        public enum SourceType
        {
            File = 0,
            String = 1,
            UnityAsset = 2
        }

        public ValidationConfig()
        {
            ruleNodeList = new Dictionary<string, XmlNodeList>();
        }

        public string viewName { get; set; }

        public Dictionary<string, XmlNodeList> ruleNodeList { get; set; }

        public void Load(string xmlSourceInfo, SourceType type = SourceType.File)
        {
            var document = new XmlDocument();
            if (type == SourceType.File)
            {
                document.Load(xmlSourceInfo);
            }
            else if (type == SourceType.UnityAsset)
            {
                BaseConfigAssetLoader loader = new UnityConfigAssetLoader();
                var xml = loader.Load(xmlSourceInfo);
                document.LoadXml(xml);
            }
            else
            {
                document.LoadXml(xmlSourceInfo);
            }

            var validationList = document.GetElementsByTagName("validation");
            foreach (XmlNode validationNode in validationList)
            foreach (XmlAttribute element in validationNode.Attributes)
                if (element.Name.Equals("viewname"))
                {
                    viewName = element.Value;
                    break;
                }

            var componentList = document.GetElementsByTagName("component");
            foreach (XmlNode componentNode in componentList)
            foreach (XmlAttribute element in componentNode.Attributes)
                if (element.Name.Equals("name"))
                    ruleNodeList.Add(element.Value, componentNode.ChildNodes);
        }
    }
}