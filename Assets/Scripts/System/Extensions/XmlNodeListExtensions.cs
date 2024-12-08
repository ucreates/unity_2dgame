using System;
using System.Xml;

namespace Core.Extensions
{
    public static class XmlNodeListExtensions
    {
        public static void ForEach(this XmlNodeList xmlNodeList, Action<XmlAttribute> action)
        {
            foreach (XmlAttribute item in xmlNodeList) action?.Invoke(item);
        }

        public static void ForEach(this XmlNodeList xmlNodeList, Func<XmlAttribute, bool> callback)
        {
            foreach (XmlAttribute item in xmlNodeList)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }
    }
}