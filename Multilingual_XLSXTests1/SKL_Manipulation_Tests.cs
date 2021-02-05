using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static Multilingual_XLSX.SKL_Manipulation;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class SKL_Manipulation_Tests
    {
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", false)]
        [DataRow("", true)]
        [DataTestMethod]
        public void IsNull_Test(string filePath, bool expectedOutcome)
        {
            XmlDocument sklDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                sklDocument.Load(filePath);
            }
            else
            {
                sklDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsNull(sklDocument));

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0)]
        [DataRow("", -1)]
        [DataTestMethod]
        public void IsSkl_Test(string filePath, int expectedOutcome)
        {
            XmlDocument sklDocument = new XmlDocument();

            if (filePath != String.Empty)
            {
                sklDocument.Load(filePath);
            }
            else
            {
                sklDocument = null;
            }

            Assert.AreEqual(expectedOutcome, IsSkl(sklDocument));
        }
    }
}