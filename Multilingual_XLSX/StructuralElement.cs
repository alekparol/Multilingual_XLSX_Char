using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class StructuralElement
    {

        /* Fields */

        protected XmlNode xNode;

        protected string sName;

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

        public XmlAttributeCollection Attributes
        {
            get
            {
                return xNode.Attributes;
            }
        }

        public string Name
        {
            get
            {
                return sName;
            }
        }  

        /* Methods */

        protected void AppendAttributeAndValue(string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = xNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            xNode.Attributes.Append(unitSize);

        }

        protected void AppendSizeUnit(string sizeUnitValue)
        {
            AppendAttributeAndValue("size-unit", sizeUnitValue);
        }

        protected void AppendCharAsSizeUnit()
        {
            AppendSizeUnit("char");
        }

        protected void AppendByteAsSizeUnit()
        {
            AppendSizeUnit("byte");
        }

        protected void AppendPixelAsSizeUnit()
        {
            AppendSizeUnit("pixel");
        }

        protected void AppendMaxWidth(string maxWidthValue)
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

        public StructuralElement()
        {

        }

        public StructuralElement(XmlNode xmlNode)
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
