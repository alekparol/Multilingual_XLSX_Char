using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutlilingual_XLSX_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string xlfPath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf";
            XmlDocument xlfDoc = new XmlDocument();
            xlfDoc.Load(xlfPath);

            Assert.AreEqual("xliff", xlfDoc.DocumentType.Name);

        }
    }
}
