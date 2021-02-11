using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Multilingual_XLSX.SKL_Manipulation;
using static Multilingual_XLSX.Utilities;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class Utilities_Tests
    {
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 319)]
        [DataTestMethod]
        public void GetCharLimitDictionary_Test(string filePath, int expectedOutcome)
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

            List<XmlNode> formattingNodesList = new List<XmlNode>();
            foreach (XmlNode formattingNode in formattingNodes)
            {
                formattingNodesList.Add(formattingNode);
            }

            Dictionary<string, string> idCharLimit = GetCharLimitDictionary(formattingNodesList);

            Assert.AreEqual(expectedOutcome, idCharLimit.Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 319)]
        [DataTestMethod]
        public void GetCharLimitDictionary2_Test(string filePath, int expectedOutcome)
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

            List<XmlNode> formattingNodesList = new List<XmlNode>();
            foreach (XmlNode formattingNode in formattingNodes)
            {
                formattingNodesList.Add(formattingNode);
            }

            Dictionary<string, Dictionary<string, string>> idCharLimit = GetCharLimitDictionary2(formattingNodesList);

            Assert.AreEqual(expectedOutcome, idCharLimit["0"].Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf")]
        [DataTestMethod]
        public void AddCharLimits_Test(string sklPath, string xlfPath)
        {
            XmlDocument sklDocument = new XmlDocument();

            if (sklPath != String.Empty)
            {
                sklDocument.Load(sklPath);
            }
            else
            {
                sklDocument = null;
            }

            XmlNodeList formattingNodes = FormattingNodesCharLimit(sklDocument);

            List<XmlNode> formattingNodesList = new List<XmlNode>();
            foreach (XmlNode formattingNode in formattingNodes)
            {
                formattingNodesList.Add(formattingNode);
            }

            Dictionary<string, string> idCharLimit = GetCharLimitDictionary(formattingNodesList);

            XmlDocument xlfDocument = new XmlDocument();

            if (xlfPath != String.Empty)
            {
                xlfDocument.Load(xlfPath);
            }
            else
            {
                xlfDocument = null;
            }

            AddCharLimits(xlfDocument, idCharLimit);

            xlfDocument.Save(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\tt.xlf");

            Assert.AreEqual(0, xlfDocument.SelectNodes("//trans-unit[@size-unit]").Count);
        }
    }
}