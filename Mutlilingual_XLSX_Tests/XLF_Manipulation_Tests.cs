using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Mutlilingual_XLSX.XLF_Manipulation;


namespace Mutlilingual_XLSX_Tests
{
    [TestClass]
    public class XLF_Manipulation_Tests
    {
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



            Assert.AreEqual("xliff", xlfDoc.DocumentType.Name);

        }
    }
}
