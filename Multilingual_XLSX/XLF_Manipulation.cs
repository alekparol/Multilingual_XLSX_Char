﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
         * Return All Nodes Specified By Name
         */
        static public XmlNodeList NodesXlf(XmlDocument xlfDocument, string tagName)
        {

            XmlNodeList nodesList = null;

            if (IsXlf(xlfDocument) == 1)
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
            return NodesXlf(xlfDocument, "trans-unit");         
        }

        /*
         * Return All Nodes Specified By Name With a Specified Value of a Specified Attribute
         */
        static public XmlNodeList NodesAttributeValueXlf(XmlDocument xlfDocument, string tagName, string attributeName, string attributeValue)
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
        static public List<XmlNode> NodesAttributeValueXlfList(XmlDocument xlfDocument, string tagName, string attributeName, string attributeValue)
        {

            XmlNodeList nodesList = NodesXlf(xlfDocument, tagName);
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

            return NodesAttributeValueXlf(xlfDocument, "trans-unit", "translate", "yes");
        }

        /*
         * Return All trans-unit Nodes with "translate" Attribute Value Set to "yes"
         */
        static public XmlNodeList TransUnitUntranslatableNodes(XmlDocument xlfDocument)
        {

            return NodesAttributeValueXlf(xlfDocument, "trans-unit", "translate", "no");
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
