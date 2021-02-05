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
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Closing Formatting\skeleton.skl", 38)]  // To Fix 
        [DataTestMethod]
        public void FormattingNodes_Test(string filePath, int expectedOutcome)
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

            XmlNodeList formattingNodes = FormattingNodes(sklDocument);

            Assert.AreEqual(expectedOutcome, formattingNodes.Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 157)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", 0)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Closing Formatting\skeleton.skl", 0)] // To Fix
        [DataTestMethod]
        public void FormattingNodesCharLimit_Test(string filePath, int expectedOutcome)
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

            XmlNodeList formattingNodes = FormattingNodesCharLimit(sklDocument);

            Assert.AreEqual(expectedOutcome, formattingNodes.Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 335)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", 36)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\skeleton.skl", 200)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\Invalid SKL\skeleton.skl", 36)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Version\skeleton.skl", 36)]
        //[DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\SKL Without Closing Formatting\skeleton.skl", 36)]  // To Fix 
        [DataTestMethod]
        public void PlaceholderNodes_Test(string filePath, int expectedOutcome)
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

            XmlNodeList placeholderNodes = PlaceholderNodes(sklDocument);

            Assert.AreEqual(expectedOutcome, placeholderNodes.Count);
        }


        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 1, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 2, 6)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 3, 7)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 4, 10)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 5, 14)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 167, 335)]
        [DataTestMethod]
        public void PlaceholderNodesPreceding_Test(string filePath, int formattingNodeId, int expectedOutcome)
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

            XmlNodeList formattingNodes = FormattingNodes(sklDocument);
            XmlNode formattingNode = formattingNodes.Item(formattingNodeId);

            List<XmlNode> placeholderNodesPreceeding = PlaceholderNodesPreceding(formattingNode);

            Assert.AreEqual(expectedOutcome, placeholderNodesPreceeding.Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 1, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 2, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 3, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 4, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 5, 4)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 167, 1)]
        [DataTestMethod]
        public void PlaceholderNodesPrecedingDirectly_Test(string filePath, int formattingNodeId, int expectedOutcome)
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

            XmlNodeList formattingNodes = FormattingNodes(sklDocument);
            XmlNode formattingNode = formattingNodes.Item(formattingNodeId);

            List<XmlNode> placeholderNodesPreceeding = PlaceholderNodesPrecedingDirectly(formattingNode);

            Assert.AreEqual(expectedOutcome, placeholderNodesPreceeding.Count);

        }

        
    }
}