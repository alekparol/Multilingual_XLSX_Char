using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class TransUnit
    {

        /* Fields */

        private string sXlf;
        private XmlDocument xXlf;

        private XmlNode nXliff;
        private XmlNodeList nTransUnit;

        /* Properties */

        /* Methods */

        public void AddAttributeAndValue(XmlNode transUnitNode, string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = transUnitNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            transUnitNode.Attributes.Append(unitSize);

        }

        static public void AddSizeUnit(XmlNode transUnitNode, string sizeUnitValue)
        {
            AddAttributeAndValue(transUnitNode, "size-unit", sizeUnitValue);
        }

        static public void AddCharAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "char");
        }

        static public void AddByteAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "byte");
        }

        static public void AddPixelAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "pixel");
        }

        static public void AddMaxWidth(XmlNode transUnitNode, string maxWidthValue)
        {

            int sizeUnit;
            bool operationValue = Int32.TryParse(maxWidthValue, out sizeUnit);

            Regex emojiEncoded = new Regex("##.*?##");

            if (operationValue)
            {

                MatchCollection encodedEmojis = emojiEncoded.Matches(transUnitNode.InnerText);

                if (encodedEmojis.Count > 0)
                {
                    foreach (Match emojiMatch in encodedEmojis)
                    {
                        sizeUnit += emojiMatch.Value.Length - 1;
                    }
                }
                AddAttributeAndValue(transUnitNode, "maxwidth", sizeUnit.ToString());
            }

        }

        static public void AddCharLimit(XmlNode transUnitNode, string maxWidthValue)
        {

            AddCharAsSizeUnit(transUnitNode);
            AddMaxWidth(transUnitNode, maxWidthValue);

        }

        /* Constructors */

        public Xlf()
        {

        }

        public Xlf(XmlDocument xmlDocument)
        {

          
        }
    }
}
