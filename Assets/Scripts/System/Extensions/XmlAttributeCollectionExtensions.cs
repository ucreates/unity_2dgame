using System;
using System.Xml;

namespace Core.Extensions
{
    public static class XmlAttributeCollectionExtensions
    {
        public static void ForEach(this XmlAttributeCollection collection, in Action<XmlAttribute> action)
        {
            if (null == collection) return;
            foreach (XmlAttribute item in collection) action?.Invoke(item);
        }

        public static void ForEach(this XmlAttributeCollection collection, in Func<XmlAttribute, bool> callback)
        {
            if (null == collection) return;
            foreach (XmlAttribute item in collection)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }
    }
}