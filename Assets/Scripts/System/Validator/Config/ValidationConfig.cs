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
using Core.Extensions;

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

        public string viewName { get; set; }

        public Dictionary<string, XmlNodeList> ruleNodeDictionary { get; set; } = new();

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
            validationList.ForEach(attribute =>
                {
                    if (attribute.Name.Equals("viewname"))
                    {
                        ruleNodeDictionary.Add(attribute.Value, validationList);
                        return false;
                    }

                    return true;
                }
            );
            var componentList = document.GetElementsByTagName("component");
            componentList.ForEach(attribute =>
                {
                    if (attribute.Name.Equals("name"))
                    {
                        ruleNodeDictionary.Add(attribute.Value, componentList);
                        return false;
                    }

                    return true;
                }
            );
        }
    }
}