using System;
using System.Xml;

namespace Core.Extensions
{
    public static class XmlNodeListExtensions
    {
        public static void ForEach(this XmlNodeList xmlNodeList, in Action<XmlAttribute> action)
        {
            foreach (XmlElement item in xmlNodeList)
            foreach (XmlAttribute attribute in item.Attributes)
                action?.Invoke(attribute);
        }

        public static void ForEach(this XmlNodeList xmlNodeList, in Func<XmlAttribute, bool> callback)
        {
            foreach (XmlElement item in xmlNodeList)
            foreach (XmlAttribute attribute in item.Attributes)
                if (!callback?.Invoke(attribute) ?? false)
                    break;
        }
    }
}