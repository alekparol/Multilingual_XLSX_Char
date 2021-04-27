using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System.Xml;

namespace Multilingual_XLSX_Tests
{
    [TestClass()]
    public class XliffStructuralElement_Tests
    {

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "source", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "target", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "formatting", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\skeleton.skl", "tu-placeholder", 1)]
        [DataTestMethod]
        public void XliffStructuralElement_Creation_Test(string filePath, string tagName, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            StructuralElement sElement = new XliffStructuralElement(nChoosen);

            Assert.IsNotNull(sElement);
            Assert.IsNotNull(sElement.XmlNode);
            Assert.AreEqual(tagName, sElement.Name);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1, "defaultUnit")]
        [DataTestMethod]
        public void AppendSizeUnit_AppendSizeUnit(string filePath, string tagName, int nodeId, string sizeUnitValue)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendSizeUnit(sizeUnitValue);

            Assert.AreEqual(attributeCount + 1, sElement.Attributes.Count);
            Assert.AreEqual(sizeUnitValue, sElement.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1)]
        [DataTestMethod]
        public void AppendSizeUnit_AppendCharAsSizeUnit(string filePath, string tagName, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendCharAsSizeUnit();

            Assert.AreEqual(attributeCount + 1, sElement.Attributes.Count);
            Assert.AreEqual("char", sElement.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1)]
        [DataTestMethod]
        public void AppendSizeUnit_AppendByteAsSizeUnit(string filePath, string tagName, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendByteAsSizeUnit();

            Assert.AreEqual(attributeCount + 1, sElement.Attributes.Count);
            Assert.AreEqual("byte", sElement.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1)]
        [DataTestMethod]
        public void AppendSizeUnit_AppendPixelAsSizeUnit(string filePath, string tagName, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendPixelAsSizeUnit();

            Assert.AreEqual(attributeCount + 1, sElement.Attributes.Count);
            Assert.AreEqual("pixel", sElement.Attributes["size-unit"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1, "100")]
        [DataTestMethod]
        public void AppendSizeUnit_AppendMaxWidth(string filePath, string tagName, int nodeId, string maxWidthValue)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendMaxWidth(maxWidthValue);

            Assert.AreEqual(attributeCount + 1, sElement.Attributes.Count);
            Assert.AreEqual(maxWidthValue, sElement.Attributes["maxwidth"].Value);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", "trans-unit", 1, "100")]
        [DataTestMethod]
        public void AppendSizeUnit_AppendCharLimit(string filePath, string tagName, int nodeId, string maxWidthValue)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName(tagName);
            XmlNode nChoosen = nList.Item(nodeId);

            XliffStructuralElement sElement = new XliffStructuralElement(nChoosen);
            int attributeCount = sElement.Attributes.Count;

            sElement.AppendCharLimit(maxWidthValue);

            Assert.AreEqual(attributeCount + 2, sElement.Attributes.Count);
            Assert.AreEqual("char", sElement.Attributes["size-unit"].Value);
            Assert.AreEqual(maxWidthValue, sElement.Attributes["maxwidth"].Value);
        }

    }
}