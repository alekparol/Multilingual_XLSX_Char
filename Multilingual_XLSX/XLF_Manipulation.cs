using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

/* TODO: Change XmlNodeList to List<XmlNode> as the return value in the sake of validation. XmlNodeList of the null object is null and cannot be set to list of length 0. */

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
         * Xliff Validity Check
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
         * Get Xliff Attribute Value
         */

        public static string XliffAttributeValue(XmlDocument xlfDocument, string attributeName)
        {
            if (IsXliffValid(xlfDocument) == 1)
            {

                string xlfVersion = String.Empty;

                XmlNode xlfNode = xlfDocument.GetElementsByTagName("xliff").Item(0);
                XmlAttribute sklVersionAttribute = xlfNode.Attributes[attributeName];

                if (sklVersionAttribute != null)
                {
                    xlfVersion = sklVersionAttribute.Value;
                }

                return xlfVersion;
            }
            else
            {
                return String.Empty;
            }
        }

        /*
         * Get Xliff Version
         */

        public static string XliffVersion(XmlDocument xlfDocument)
        {
            return XliffAttributeValue(xlfDocument, "version");
        }

        /*
         * Get Xliff Xmlns
         */

        public static string XliffXmlns(XmlDocument xlfDocument)
        {
            return XliffAttributeValue(xlfDocument, "xmlns:logoport");
        }

        /*
         * content.xlf Check
         */
        public static bool IsContent(XmlDocument xlfDocument)
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
         * Return All Nodes Specified By Name
         */
        static public XmlNodeList NodesXliff(XmlDocument xlfDocument, string tagName)
        {

            XmlNodeList nodesList = null;

            if (IsXliff(xlfDocument) == 1)
            {
                nodesList = xlfDocument.GetElementsByTagName(tagName);
            }

            return nodesList;
        }

        /*
         * Return All trans-unit Nodes
         */
        static public XmlNodeList TransUnitNodes(XmlDocument xlfDocument)
        {
            return NodesXliff(xlfDocument, "trans-unit");         
        }

        /*
         * Return All Nodes Specified By Name With a Specified Value of a Specified Attribute
         */
        static public XmlNodeList NodesAttributeValueXliff(XmlDocument xlfDocument, string tagName, string attributeName, string attributeValue)
        {

            XmlNodeList transUnitList = null;        

            if (IsContent(xlfDocument))
            {
                string xpathTagName = "//" + tagName;
                string xpathAttributeValue = "[@" + attributeName + "=\'" + attributeValue + "\']";

                transUnitList = xlfDocument.SelectNodes(xpathTagName + xpathAttributeValue);
            }

            return transUnitList;
        }

        /*
         * Return List of All Nodes Specified By Name With a Specified Value of a Specified Attribute
         */
        static public List<XmlNode> NodesAttributeValueXliffList(XmlDocument xlfDocument, string tagName, string attributeName, string attributeValue)
        {

            XmlNodeList nodesList = NodesXliff(xlfDocument, tagName);
            List<XmlNode> nodesListLimited = new List<XmlNode>();

            if (nodesList != null)
            {
                foreach (XmlNode node in nodesList)
                {
                    if (node.Attributes[attributeName].Value == attributeValue)
                    {
                        nodesListLimited.Add(node);
                    }
                }
            }

            return nodesListLimited;
        }

        /*
         * Return All trans-unit Nodes with "translate" Attribute Value Set to "yes"
         */
        static public XmlNodeList TransUnitTranslatableNodes(XmlDocument xlfDocument)
        {

            return NodesAttributeValueXliff(xlfDocument, "trans-unit", "translate", "yes");
        }

        /*
         * Return All trans-unit Nodes with "translate" Attribute Value Set to "yes"
         */
        static public XmlNodeList TransUnitUntranslatableNodes(XmlDocument xlfDocument)
        {

            return NodesAttributeValueXliff(xlfDocument, "trans-unit", "translate", "no");
        }

        /*
         * Adds a group Node Previous to the First Node in the List and then Reappend All Nodes from the List to this Element 
         */
        static public void GroupNodes(XmlNodeList transUnitNodes)
        {
            if (transUnitNodes != null)
            {

                XmlNode firstNode = transUnitNodes.Item(0); // Find First Node from the List

                XmlNode parentNode = firstNode.ParentNode;
                XmlElement groupNode = parentNode.OwnerDocument.CreateElement("group");

                parentNode.InsertBefore(groupNode, firstNode); // Append group Node Before First Node from the List

                foreach (XmlNode transUnitNode in transUnitNodes)
                {
                    parentNode.RemoveChild(transUnitNode);
                    groupNode.AppendChild(transUnitNode);
                }
            }
        }

        static public void GroupNodes(List<XmlNode> transUnitNodes)
        {
            if (transUnitNodes != null)
            {

                XmlNode firstNode = transUnitNodes.ElementAt(0); // Find First Node from the List

                XmlNode parentNode = firstNode.ParentNode;
                XmlElement groupNode = parentNode.OwnerDocument.CreateElement("group");

                parentNode.InsertBefore(groupNode, firstNode); // Append group Node Before First Node from the List

                foreach (XmlNode transUnitNode in transUnitNodes)
                {
                    parentNode.RemoveChild(transUnitNode);
                    groupNode.AppendChild(transUnitNode);
                }
            }
        }

        /*
         * Adds Attribute and its Value to the Passed XmlNode [XML]
         */
        static public void AddAttributeAndValue(XmlNode transUnitNode, string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = transUnitNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            transUnitNode.Attributes.Append(unitSize);

        }

        /*
         * Adds size-unit and its Passed Value to the Passed XmlNode [XLF]
         */
        static public void AddSizeUnit(XmlNode transUnitNode, string sizeUnitValue)
        {
            AddAttributeAndValue(transUnitNode, "size-unit", sizeUnitValue);
        }

        /*
         * Adds size-unit and its Value Equal to char to the Passed XmlNode [XLF]
         */
        static public void AddCharAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "char");
        }

        /*
         * Adds size-unit and its Value Equal to byte to the Passed XmlNode [XLF]
         */
        static public void AddByteAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "byte");
        }

        /*
         * Adds size-unit and its Value Equal to pixel to the Passed XmlNode [XLF]
         */
        static public void AddPixelAsSizeUnit(XmlNode transUnitNode)
        {
            AddSizeUnit(transUnitNode, "pixel");
        }

        /*
         * Adds maxwidth Attribute and its Value Equal to Passed String to the Passed XmlNode [XLF]
         */
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
                    foreach(Match emojiMatch in encodedEmojis)
                    {
                        sizeUnit += emojiMatch.Value.Length - 1;
                    }
                }
                AddAttributeAndValue(transUnitNode, "maxwidth", sizeUnit.ToString());
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
