using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System.Xml;

namespace Multilingual_XLSX_Tests
{
    [TestClass()]
    public class StructuralElement_Tests
    {

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "source", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "target", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 1)]
        [DataTestMethod]
        public void StructuralElement_Creation_Test(string filePath, string tagName, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.IsNotNull(sElement);
            Assert.IsNotNull(sElement.XmlNode);
            Assert.AreEqual(tagName, sElement.Name);
        }


        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1, "body")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "source", 1, "trans-unit")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "target", 1, "trans-unit")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 1, "body")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 1, "body")]
        [DataTestMethod]
        public void StructuralElement_ParentNode_Test(string filePath, string tagName, int nodeId, string expectedParentNodeName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedParentNodeName, sElement.ParentNode.Name);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 2)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "source", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 1, 1)]
        [DataTestMethod]
        public void StructuralElement_Attributes_Test(string filePath, string tagName, int nodeId, int expectedAttributesCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedAttributesCount, sElement.Attributes.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 19)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 6)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 7)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 73)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 69)]
        [DataTestMethod]
        public void StructuralElement_ElementsPreceding_Test(string filePath, string tagName, int nodeId, int expectedPrecedingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedPrecedingCount, sElement.ElementsPreceding.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 19)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 37)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 35)]
        [DataTestMethod]
        public void StructuralElement_ElementsPrecedingSameName_Test(string filePath, string tagName, int nodeId, int expectedPrecedingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedPrecedingCount, sElement.ElementsPrecedingSameName.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 16)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 73)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 72)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 67)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 66)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 4)]
        [DataTestMethod]
        public void StructuralElement_ElementsFollowing_Test(string filePath, string tagName, int nodeId, int expectedFollowingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedFollowingCount, sElement.ElementsFollowing.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 16)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 37)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 34)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 32)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 0)]
        [DataTestMethod]
        public void StructuralElement_ElementsFollowingSameName_Test(string filePath, string tagName, int nodeId, int expectedFollowingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedFollowingCount, sElement.ElementsFollowingSameName.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, "someAttribute", "someValue", 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "source", 0, "someAttribute", "someValue", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "target", 0, "someAttribute", "someValue", 4)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, "someAttribute", "someValue", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, "someAttribute", "someValue", 2)]
        [DataTestMethod]
        public void StructuralElement_AppendAttributeAndValue_Test(string filePath, string tagName, int nodeId, string appendedAttribute, string appendedValue, int expectedAttributesCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);
            sElement.AppendAttributeValue(appendedAttribute, appendedValue);

            Assert.AreEqual(expectedAttributesCount, sElement.Attributes.Count);
            Assert.AreEqual(appendedValue, sElement.Attributes[appendedAttribute].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 19)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 37)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 35)]
        [DataTestMethod]
        public void StructuralElement_ElementsPrecedingTagName_Test(string filePath, string tagName, int nodeId, int expectedPrecedingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedPrecedingCount, sElement.ElementsPrecedingTagName(tagName).Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 0, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 19, 16)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 35, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 0, 37)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 0, 35)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 3, 34)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 3, 32)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 37, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 35, 0)]
        [DataTestMethod]
        public void StructuralElement_ElementsFollowingTagName_Test(string filePath, string tagName, int nodeId, int expectedFollowingCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new StructuralElement(nChoosen);

            Assert.AreEqual(expectedFollowingCount, sElement.ElementsFollowingTagName(tagName).Count);
        }
    }
}
