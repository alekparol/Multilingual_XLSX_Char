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

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "version", "1.0")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "notExistingAttribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", "version", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", "whateverAttributeName", "")] // To Fix 
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "", "")] // To Fix 
        [DataRow("", "", "")]
        [DataTestMethod]
        public void SklAttributeValue_Test(string filePath, string attributeName, string expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, SklAttributeValue(sklDocument, attributeName));
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

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", true)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", false)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", false)]
        [DataRow("", false)]
        [DataTestMethod]
        [TestMethod()]
        public void IsSkeleton_Test(string filePath, bool expectedOutcome)
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

            Assert.AreEqual(expectedOutcome, IsSkeleton(sklDocument));
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 38)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\skeleton.skl", 124)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", 38)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", 38)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Closing Formatting\skeleton.skl", 38)]  // To Fix 
        [DataTestMethod]
        public void GetAllFormattingNodes_Test(string filePath, int expectedOutcome)
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

            XmlNodeList formattingNodes = GetAllFormattingNodes(sklDocument);

            Assert.AreEqual(expectedOutcome, formattingNodes.Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 157)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Closing Formatting\skeleton.skl", 0)] // To Fix
        [DataTestMethod]
        public void GetFormattingNodesContainingMaxChar_Test(string filePath, int expectedOutcome)
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

            XmlNodeList formattingNodes = GetFormattingNodesContainingMaxChar(sklDocument);

            Assert.AreEqual(expectedOutcome, formattingNodes.Count);

        }
    }
}