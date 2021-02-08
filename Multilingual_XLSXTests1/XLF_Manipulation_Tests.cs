using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void IsXlf_Test(string filePath, int expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, IsXlf(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", -1)]
        [DataRow("", -1)]
        [DataTestMethod]
        public void IsXlfValid_Test(string filePath, int expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, IsXlfValid(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "version", "1.1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "notExistingAttribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", "version", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "whateverAttributeName", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "", "")] // To Fix 
        [DataRow("", "", "")]
        [DataTestMethod]
        public void XlfAttributeValue_Test(string filePath, string attributeName, string expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, XlfAttributeValue(xlfDocument, attributeName));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "1.1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "")] // To Fix 
        [DataRow("", "")]
        [DataTestMethod]
        public void XlfVersion_Test(string filePath, string expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, XlfVersion(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "urn:logoport:xliffeditor:xliff-extras:1.0")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without XMLNS\content.xlf", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "")] // To Fix 
        [DataRow("", "")]
        [DataTestMethod]
        public void XlfXmlns_Test(string filePath, string expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, XlfXmlns(xlfDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", true)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without Version\content.xlf", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\XLF Without XMLNS\content.xlf", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid XLF\content.xlf", false)] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", false)] // To Fix 
        [DataRow("", false)]
        [DataTestMethod]
        public void IsContentXlf_Test(string filePath, bool expectedOutcome)
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

        /*[TestMethod()]
        public void AddCharLimit_Test()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestMethod1()
        {

            string xlfPath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf";
            XmlDocument xlfDoc = new XmlDocument();
            xlfDoc.Load(xlfPath);

            Assert.AreEqual("xliff", xlfDoc.DocumentType.Name);

        }

        [TestMethod]
        public void TestMethod2()
        {

            string xlfPath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf";
            XmlDocument xlfDoc = new XmlDocument();
            xlfDoc.Load(xlfPath);

            XmlNodeList transUnits = GetTranslatableTransUnitNodes(xlfDoc);

            XmlNode tt =
            AddCharLimit(transUnits.Item(0), "200");

            Assert.AreEqual("xliff", transUnits.Item(0).OuterXml);

        }*/
    }
}