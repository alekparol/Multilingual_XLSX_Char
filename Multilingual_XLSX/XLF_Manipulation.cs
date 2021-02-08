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
         * Xlf Check
         */
        public static int IsXlf(XmlDocument xlfDocument)
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
         * Xlf Validity Check
         */

        public static int IsXlfValid(XmlDocument xlfDocument)
        {
            if (IsXlf(xlfDocument) == 1)
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
         * Get Xlf Attribute Value
         */

        public static string XlfAttributeValue(XmlDocument xlfDocument, string attributeName)
        {
            if (IsXlfValid(xlfDocument) == 1)
            {

                string sklVersion = String.Empty;

                XmlNode sklNode = xlfDocument.GetElementsByTagName("xliff").Item(0);
                XmlAttribute sklVersionAttribute = sklNode.Attributes[attributeName];

                if (sklVersionAttribute != null)
                {
                    sklVersion = sklVersionAttribute.Value;
                }

                return sklVersion;
            }
            else
            {
                return String.Empty;
            }
        }

        /*
         * Get Xlf Version
         */

        public static string XlfVersion(XmlDocument xlfDocument)
        {
            return XlfAttributeValue(xlfDocument, "version");
        }

        /*
         * Get Xlf Xmlns
         */

        public static string XlfXmlns(XmlDocument xlfDocument)
        {
            return XlfAttributeValue(xlfDocument, "xmlns:logoport");
        }

        /*
         * content.xlf Check
         */
        public static bool IsContent(XmlDocument xlfDocument)
        {
            if (XlfVersion(xlfDocument) != String.Empty && XlfXmlns(xlfDocument) != String.Empty)
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

            if (IsContent(xlfDocument))
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

            if (IsContent(xlfDocument))
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
        static public void AddCharAsSizeUnit(XmlNode transUnitNode)
        {

            AddSizeUnit(transUnitNode, "char");

        }

        /*
         * 
         */
        static public void AddByteAsSizeUnit(XmlNode transUnitNode)
        {

            AddSizeUnit(transUnitNode, "byte");

        }

        /*
         * 
         */
        static public void AddPixelAsSizeUnit(XmlNode transUnitNode)
        {

            AddSizeUnit(transUnitNode, "pixel");

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
                AddAttributeAndValue(transUnitNode, "maxwidth", maxWidthValue);
            }

        }

        /*
         * 
         */
        static public void AddCharLimit(XmlNode transUnitNode, string maxWidthValue)
        {

            AddCharAsSizeUnit(transUnitNode);
            AddMaxWidth(transUnitNode, maxWidthValue);

        }

        /*
         * To be continued
         */

    }
}
