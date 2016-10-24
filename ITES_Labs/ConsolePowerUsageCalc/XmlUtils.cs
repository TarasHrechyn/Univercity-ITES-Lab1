using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace ConsolePowerUsageCalc
{
    
    // приклад допоміжного класу зі статичними методами
    static class XmlUtils
    {
        static public string DecimalSeparator
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator; }
        }


        public static XmlNode CreateDocument(string nodeName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode result = doc.CreateElement(nodeName);
            doc.AppendChild(result);
            return result;
        }
        public static XmlNode CreateSubNode(XmlNode parent, string nodeName)
        {
            XmlNode result = parent.OwnerDocument.CreateElement(nodeName);
            parent.AppendChild(result);
            return result;
        }

        public static void SetAttribute(XmlNode node, string attributeName, string value, string defaultValue = "")
        {
            if (value != defaultValue)
            {
                (node as XmlElement).SetAttribute(attributeName, value);
            } else
            {
                (node as XmlElement).RemoveAttribute(attributeName);
            }
        }
        public static void SetAttribute(XmlNode node, string attributeName, double value, double defaultValue = 0.0)
        {
            SetAttribute(node, attributeName, Convert.ToString(value), Convert.ToString(defaultValue));
        }

        public static string GetAttribute(XmlNode node, string attributeName, string defaultValue = "")
        {
            XmlAttribute attr = node.Attributes[attributeName];
            return attr == null || attr.Value == null ? defaultValue : attr.Value;
        }
        public static int GetAttribute(XmlNode node, string attributeName, int defaultValue = 0)
        {
            int value;
            XmlAttribute att = node.Attributes[attributeName];
            if ((att != null) && int.TryParse(att.Value, out value))
                return value;
            return defaultValue;
        }
        public static double GetAttribute(XmlNode node, string attributeName, double defaultValue = 0.0)
        {
            double value;
            XmlAttribute att = node.Attributes[attributeName];
            if (att == null)
                return defaultValue;
            else
                return double.TryParse(att.Value.Replace(".",  DecimalSeparator), out value) ? value : defaultValue;
        }

        public static bool GetAttribute(XmlNode node, string attributeName, bool defaultValue = false)
        {
            bool value;
            XmlAttribute att = node.Attributes[attributeName];
            if ((att == null) && bool.TryParse(att.Value, out value))
                return value;
            return defaultValue;
        }
    }
}
