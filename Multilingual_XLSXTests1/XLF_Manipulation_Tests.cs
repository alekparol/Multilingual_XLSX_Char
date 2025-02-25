﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static Multilingual_XLSX.XLF_Manipulation;
using System.Linq;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class XLF_Manipulation_Tests
    {

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", false)]
        [DataRow("", true)]
        [DataTestMethod]
        public void IsNull_Test(string filePath, bool expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsNull(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataRow("", -1)]
        [DataTestMethod]
        public void IsXliff_Test(string filePath, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsXliff(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", -1)]
        [DataRow("", -1)]
        [DataTestMethod]
        public void IsXliffValid_Test(string filePath, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsXliffValid(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "version", "1.1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "notExistingAttribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", "version", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "whateverAttributeName", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "", "")] // To Fix 
        [DataRow("", "", "")]
        [DataTestMethod]
        public void XliffAttributeValue_Test(string filePath, string attributeName, string expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, XliffAttributeValue(xlfDocument, attributeName));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "1.1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "")] // To Fix 
        [DataRow("", "")]
        [DataTestMethod]
        public void XliffVersion_Test(string filePath, string expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, XliffVersion(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "urn:logoport:xliffeditor:xliff-extras:1.0")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without XMLNS\content.xlf", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "")] // To Fix 
        [DataRow("", "")]
        [DataTestMethod]
        public void XliffXmlns_Test(string filePath, string expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, XliffXmlns(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", true)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without XMLNS\content.xlf", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", false)] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", false)] // To Fix 
        [DataRow("", false)]
        [DataTestMethod]
        public void IsContent_Test(string filePath, bool expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsContent(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 36)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "target", 25)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "bpt", 46)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "ept", 44)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "ph", 5)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "it", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "body", 1)]
        [DataTestMethod]
        public void NodesXliff_Test(string filePath, string tagName, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesXliff = NodesXliff(xlfDocument, tagName);

            Assert.AreEqual(expectedOutcome, nodesXliff.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 36)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", 36)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without XMLNS\content.xlf", 36)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)] 
        //[DataRow("", 0)]
        [DataTestMethod]
        public void TransUnitNodes_Test(string filePath, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList transUnitNotes = TransUnitNodes(xlfDocument);

            Assert.AreEqual(expectedOutcome, transUnitNotes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "no", 12)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "yes", 24)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "bpt", "id", "1", 30)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)] 
        //[DataRow("", 0)]
        [DataTestMethod]
        public void NodesAttributeValueXliff_Test(string filePath, string tagName, string attributeName, string attributeValue, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = NodesAttributeValueXliff(xlfDocument, tagName, attributeName, attributeValue);

            Assert.AreEqual(expectedOutcome, nodesWithAttributes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "no", 12)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "yes", 24)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", "translate", "", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "bpt", "id", "1", 30)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)] 
        //[DataRow("", 0)]
        [DataTestMethod]
        public void NodesAttributeValueXliffList_Test(string filePath, string tagName, string attributeName, string attributeValue, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            List<XmlNode> nodesWithAttributes = NodesAttributeValueXliffList(xlfDocument, tagName, attributeName, attributeValue);

            Assert.AreEqual(expectedOutcome, nodesWithAttributes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 24)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)] 
        //[DataRow("", 0)]
        [DataTestMethod]
        public void TransUnitTranslatableNodes_Test(string filePath, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitTranslatableNodes(xlfDocument);

            Assert.AreEqual(expectedOutcome, nodesWithAttributes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 12)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)] 
        //[DataRow("", 0)]
        [DataTestMethod]
        public void TransUnitUntranslatableNodes_Test(string filePath, int expectedOutcome)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);

            Assert.AreEqual(expectedOutcome, nodesWithAttributes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf")]
        [DataTestMethod]
        public void GroupNodes_Test(string filePath)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);

            GroupNodes(nodesWithAttributes);
            XmlNode groupNode = xlfDocument.SelectSingleNode("//group");

            Assert.AreEqual(nodesWithAttributes.Count, groupNode.ChildNodes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "maxwidth", "100")]
        [DataTestMethod]
        public void AddAttributeAndValue_Test(string filePath, int nodeId, string attributeName, string attributeValue)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddAttributeAndValue(nodeFromList, attributeName, attributeValue);

            Assert.AreEqual(attributeValue, nodeFromList.Attributes[attributeName].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "char")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "byte")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "pixel")]
        [DataTestMethod]
        public void AddSizeUnit_Test(string filePath, int nodeId, string sizeUnitValue)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddSizeUnit(nodeFromList, sizeUnitValue);

            Assert.AreEqual(sizeUnitValue, nodeFromList.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataTestMethod]
        public void AddCharAsSizeUnit_Test(string filePath, int nodeId)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddCharAsSizeUnit(nodeFromList);

            Assert.AreEqual("char", nodeFromList.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataTestMethod]
        public void AddByteAsSizeUnit_Test(string filePath, int nodeId)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddByteAsSizeUnit(nodeFromList);

            Assert.AreEqual("byte", nodeFromList.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataTestMethod]
        public void AddPixelAsSizeUnit_Test(string filePath, int nodeId)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddPixelAsSizeUnit(nodeFromList);

            Assert.AreEqual("pixel", nodeFromList.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "100")]
        [DataTestMethod]
        public void AddMaxWidth_Test(string filePath, int nodeId, string maxWidthValue)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddMaxWidth(nodeFromList, maxWidthValue);

            Assert.AreEqual(maxWidthValue, nodeFromList.Attributes["maxwidth"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, "100")]
        [DataTestMethod]
        public void AddCharLimit_Test(string filePath, int nodeId, string maxWidthValue)
        {
            XmlDocument xlfDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                xlfDocument.Load(filePath);
            }
            else
            {
                xlfDocument = null;
            }

            XmlNodeList nodesWithAttributes = TransUnitUntranslatableNodes(xlfDocument);
            XmlNode nodeFromList = nodesWithAttributes.Item(nodeId);

            AddCharLimit(nodeFromList, maxWidthValue);

            Assert.AreEqual("char", nodeFromList.Attributes["size-unit"].Value);
            Assert.AreEqual(maxWidthValue, nodeFromList.Attributes["maxwidth"].Value);
        }
    }
}