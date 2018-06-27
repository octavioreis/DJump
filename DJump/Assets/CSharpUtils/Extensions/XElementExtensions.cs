using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public static class XElementExtensions
{
    public static XAttribute GetAttribute(this IEnumerable<XAttribute> attributes, string attributeName)
    {
        return attributes.FirstOrDefault(e => string.Equals(e.Name.LocalName, attributeName, StringComparison.OrdinalIgnoreCase));
    }

    public static string GetAttributeValue(this IEnumerable<XAttribute> attributes, string attributeName)
    {
        var attribute = attributes.GetAttribute(attributeName);
        if (attribute != null)
            return attribute.Value;

        return string.Empty;
    }

    public static XElement GetElement(this IEnumerable<XElement> elements, string elementName)
    {
        return elements.FirstOrDefault(e => string.Equals(e.Name.LocalName, elementName, StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<XElement> GetElements(this IEnumerable<XElement> elements, string elementName)
    {
        return elements.Where(e => string.Equals(e.Name.LocalName, elementName, StringComparison.OrdinalIgnoreCase));
    }
}
