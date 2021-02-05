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

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", -1)]
        [DataRow("", -1)]
        [DataTestMethod]
        public void IsSklValid_Test(string filePath, int expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, IsSklValid(sklDocument));

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "1.0")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "")]
        [DataRow("", "")]
        [DataTestMethod]
        public void SklVersion_Test(string filePath, string expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, SklVersion(sklDocument));

        }
    }
}