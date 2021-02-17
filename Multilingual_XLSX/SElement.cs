using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class SElement
    {

        /* Fields */

        private XmlNode xNode;

        private string sName;

        /* Properties */

        public XmlNode XmlNode
        {
            get
            {
                return xNode;
            }
        }

        public XmlNode ParentNode
        {
            get
            {
                return xNode.ParentNode;
            }
        }

        public string Name
        {
            get
            {
                return sName;
            }
        }

        public XmlAttributeCollection Attributes
        {
            get
            {
                return xNode.Attributes;
            }
        }

        public string Id
        {
            get
            {
                if (xNode.Attributes["id"] != null)
                {
                    return xNode.Attributes["id"].Value;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string Translate
        {
            get
            {
                if (xNode.Attributes["translate"] != null)
                {
                    return xNode.Attributes["translate"].Value;
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        /* Methods */

        public void AppendAttributeAndValue(string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = xNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            xNode.Attributes.Append(unitSize);

        }

        public void AppendSizeUnit(string sizeUnitValue)
        {
            AppendAttributeAndValue("size-unit", sizeUnitValue);
        }

        public void AppendCharAsSizeUnit()
        {
            AppendSizeUnit("char");
        }

        public void AppendByteAsSizeUnit()
        {
            AppendSizeUnit("byte");
        }

        public void AppendPixelAsSizeUnit()
        {
            AppendSizeUnit("pixel");
        }

        public void AppendMaxWidth(string maxWidthValue)
        {

            int sizeUnit;
            bool operationValue = Int32.TryParse(maxWidthValue, out sizeUnit);

            Regex emojiEncoded = new Regex("##.*?##");

            if (operationValue)
            {
                MatchCollection encodedEmojis = emojiEncoded.Matches(xNode.InnerText);

                if (encodedEmojis.Count > 0)
                {
                    foreach (Match emojiMatch in encodedEmojis)
                    {
                        sizeUnit += emojiMatch.Value.Length - 1;
                    }
                }
                AppendAttributeAndValue("maxwidth", sizeUnit.ToString());
            }

        }

        public void AppendCharLimit(string maxWidthValue)
        {
            AppendCharAsSizeUnit();
            AppendMaxWidth(maxWidthValue);
        }

        /* Constructors */

        public XElement()
        {

        }

        public XElement(XmlNode xmlNode)
        {

            xNode = xmlNode;

            if (xmlNode != null)
            {
                sName = xmlNode.Name;

                if (sName == String.Empty)
                {
                    throw new Exception(String.Format("Creation of a new XElement object cannot be done. The XmlNode Name is empty."));
                }
            }
            else
            {
                throw new Exception(String.Format("Creation of a new XElement object cannot be done. The XmlNode is null."));
            }
        }

    }
}
