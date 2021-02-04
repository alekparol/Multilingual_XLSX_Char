using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static Multilingual_XLSX.XLF_Manipulation;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class XLF_Manipulation_Tests
    {
        [TestMethod()]
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

            /*XmlNode tt = */AddCharLimit(transUnits.Item(0), "200");

            Assert.AreEqual("xliff", transUnits.Item(0).OuterXml);

        }
    }
}