using System;
using System.Xml;

namespace Core.Extensions
{
    public static class XmlAttributeCollectionExtensions
    {
        public static void ForEach(this XmlAttributeCollection collection, Action<XmlNode> action)
        {
            foreach (XmlNode item in collection) action?.Invoke(item);
        }

        public static void ForEach(this XmlAttributeCollection collection, Func<XmlNode, bool> callback)
        {
            foreach (XmlNode item in collection)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }
    }
}