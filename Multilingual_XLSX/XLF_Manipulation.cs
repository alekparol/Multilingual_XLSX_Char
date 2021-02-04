using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Multilingual_XLSX
{
    public class XLF_Manipulation
    {

        static string[] sizeUnit = { "pixel", "byte", "char" };

        /*
         * Null Check
         */

        public static bool IsNull(XmlDocument xlfDocument)
        {
            if (xlfDocument == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Xliff Check
         */
        public static int IsXliff(XmlDocument xlfDocument)
        {
            if (!IsNull(xlfDocument))
            {
                if (xlfDocument.DocumentType.Name.Equals("xliff"))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        /*
         * Xliff Smoke Check
         */

        public static int IsXliffValid(XmlDocument xlfDocument)
        {
            if (IsXliff(xlfDocument) == 1)
            {
                XmlNodeList xliffNodes = xlfDocument.GetElementsByTagName("xliff");

                if (xliffNodes.Count == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        /*
         * Get Xliff Version
         */

        public static string XliffVersion(XmlDocument xlfDocument)
        {
            if (IsXliffValid(xlfDocument) == 1)
            {
                XmlNode xliffNode = xlfDocument.GetElementsByTagName("xliff").Item(0);
                string xliffVersion = xliffNode.Attributes["version"].Value;

                return xliffVersion;
            }
            else
            {
                return String.Empty;
            }
        }

        /*
         * Get Xliff Xmlns
         */

        public static string XliffXmlns(XmlDocument xlfDocument)
        {
            if (IsXliffValid(xlfDocument) == 1)
            {
                XmlNode xliffNode = xlfDocument.GetElementsByTagName("xliff").Item(0);
                string xliffVersion = xliffNode.Attributes["xmlns:logoport"].Value;

                return xliffVersion;
            }
            else
            {
                return String.Empty;
            }
        }

        /*
         * content.xlf Check
         */
        public static bool IsContentXlf(XmlDocument xlfDocument)
        {
            if (XliffVersion(xlfDocument) != String.Empty && XliffXmlns(xlfDocument) != String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
         * 
         */
        static public XmlNodeList GetAllTransUnitNodes(XmlDocument xlfDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsContentXlf(xlfDocument))
            {
                transUnitList = xlfDocument.GetElementsByTagName("trans-unit");
            }

            return transUnitList;
        }

        /*
         * 
         */
        static public XmlNodeList GetTranslatableTransUnitNodes(XmlDocument xlfDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsContentXlf(xlfDocument))
            {
                transUnitList = xlfDocument.SelectNodes("//trans-unit[@translate='yes']");
            }

            return transUnitList;
        }

        /*
         * 
         */
        static public void AddAttributeAndValue(XmlNode transUnitNode, string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = transUnitNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            transUnitNode.Attributes.Append(unitSize);

        }

        /*
         * 
         */
        static public void AddSizeUnit(XmlNode transUnitNode, string sizeUnitValue)
        {

            if (sizeUnit.Contains(sizeUnitValue))
            {
                AddAttributeAndValue(transUnitNode, "size-unit", sizeUnitValue);
            }

        }

        /*
         * 
         */
        static public void AddMaxWidth(XmlNode transUnitNode, string maxWidthValue)
        {

            int sizeUnit;
            bool operationValue = Int32.TryParse(maxWidthValue, out sizeUnit);

            if (operationValue)
            {
                AddAttributeAndValue(transUnitNode, "size-unit", maxWidthValue);
            }

        }

        /*
         * 
         */
        static public void AddCharLimit(XmlNode transUnitNode, string limitValue)
        {

            XmlAttribute unitSize = transUnitNode.OwnerDocument.CreateAttribute("size-unit");
            unitSize.Value = "char";

            XmlAttribute maxWidth = transUnitNode.OwnerDocument.CreateAttribute("maxwidth");
            maxWidth.Value = limitValue;

            transUnitNode.Attributes.Append(unitSize);
            transUnitNode.Attributes.Append(maxWidth);

            //return transUnitNode;

        }

        /*
         * To be continued
         */

    }
}
