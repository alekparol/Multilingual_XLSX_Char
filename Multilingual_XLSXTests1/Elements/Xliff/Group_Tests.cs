using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using static Multilingual_XLSX.SKL_Manipulation;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class Group_Tests
    {
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1)]
        [DataTestMethod]
        public void Group_Creation_Test(string filePath, int nodeId)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList nList = xmlDocument.GetElementsByTagName("group");
            XmlNode nChoosen = nList.Item(nodeId);

            Group sElement = new Group(nChoosen);

            Assert.IsNotNull(sElement);
            Assert.IsNotNull(sElement.XmlNode);

        }
    }
}