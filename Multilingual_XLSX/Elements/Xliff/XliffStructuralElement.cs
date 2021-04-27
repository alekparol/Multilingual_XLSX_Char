using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class XliffStructuralElement : StructuralElement
    {

        /* Fields */

        /* Properties */

        /* Methods */

        public void AppendSizeUnit(string attributeValue)
        {
            AppendAttributeValue("size-unit", attributeValue);
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

        public void AppendMaxWidth(string attributeValue)
        {

            int sizeUnit;
            bool operationValue = Int32.TryParse(attributeValue, out sizeUnit);

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
                AppendAttributeValue("maxwidth", sizeUnit.ToString());
            }

        }

        public void AppendCharLimit(string attributeValue)
        {
            AppendCharAsSizeUnit();
            AppendMaxWidth(attributeValue);
        }

        /* Constructors */

        public XliffStructuralElement()
        {

        }

        public XliffStructuralElement(XmlNode xmlNode) : base(xmlNode)
        {

        }

    }
}
