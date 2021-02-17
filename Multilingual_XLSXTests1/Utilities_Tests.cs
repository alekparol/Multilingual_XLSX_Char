using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multilingual_XLSX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Multilingual_XLSX.SKL_Manipulation;
using static Multilingual_XLSX.XLF_Manipulation;
using static Multilingual_XLSX.XLZ_Manipulation;
using static Multilingual_XLSX.Utilities;
using System.IO;

namespace Multilingual_XLSX.Tests
{
    [TestClass()]
    public class Utilities_Tests
    {

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 0, 3, "1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 1, 3, "4")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 2, 1, "7")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 3, 3, "8")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 4, 4, "11")]
        [DataTestMethod]
        public void CharLimitDictionaryPerFormatting_Test(string sklPath, int formattingNodeId, int excpetedOutcome, string excpectedKey)
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

            /**/

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument));
            Dictionary<string, string> charLimitDictionary = CharLimitDictionaryPerFormatting(formattingNodes[formattingNodeId]);

            Assert.AreEqual(excpetedOutcome, charLimitDictionary.Count);
            Assert.IsTrue(charLimitDictionary.ContainsKey(excpectedKey));

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 0, 157, 157)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 0, 3, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", 5, 10, 10)]
        [DataTestMethod]
        public void CharLimitDictionary_Test(string sklPath, int startIndex, int rangeValue, int excpetedKeyCount)
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

            /**/

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument)).GetRange(startIndex, rangeValue);
            Dictionary<int, Dictionary<string, string>> charLimitDictionary = CharLimitDictionary(formattingNodes);

            Assert.AreEqual(excpetedKeyCount, charLimitDictionary.Keys.Count);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 1, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 2, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 3, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 4, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 5, 1)]
        [DataTestMethod]
        public void AddCharLimitsSingle_Test(string sklPath, string xlfPath, int formattingNodeId, int expectedOutcome)
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

            XmlDocument xlfDocument = new XmlDocument();

            if (xlfPath != String.Empty)
            {
                xlfDocument.Load(xlfPath);
            }
            else
            {
                xlfDocument = null;
            }

            /**/

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument));
            List<XmlNode> transUnitNodes = XmlNodeListToList(TransUnitNodes(xlfDocument));

            Dictionary<int, Dictionary<string, string>> charLimitDictionary = CharLimitDictionary(formattingNodes);
            AddCharLimitsSingle(transUnitNodes, charLimitDictionary[formattingNodeId]);

            Assert.AreEqual(expectedOutcome, xlfDocument.SelectNodes("//*[@size-unit='char']").Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 1, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 2, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 3, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 4, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 5, 0)]
        [DataTestMethod]
        public void AddCharLimitsMultiple_Test(string sklPath, string xlfPath, int formattingNodeId, int expectedOutcome)
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

            XmlDocument xlfDocument = new XmlDocument();

            if (xlfPath != String.Empty)
            {
                xlfDocument.Load(xlfPath);
            }
            else
            {
                xlfDocument = null;
            }

            /**/

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument));
            List<XmlNode> transUnitNodes = XmlNodeListToList(TransUnitNodes(xlfDocument));

            Dictionary<int, Dictionary<string, string>> charLimitDictionary = CharLimitDictionary(formattingNodes);
            AddCharLimitsMultiple(transUnitNodes, charLimitDictionary[formattingNodeId]);

            Assert.AreEqual(expectedOutcome, xlfDocument.SelectNodes("//*[@size-unit='char']").Count);

            xlfDocument.Save(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\tt.xlf");
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 1, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 2, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 3, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 4, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", 5, 1)]
        [DataTestMethod]
        public void AddCharLimits_Test(string sklPath, string xlfPath, int formattingNodeId, int expectedOutcome)
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

            XmlDocument xlfDocument = new XmlDocument();

            if (xlfPath != String.Empty)
            {
                xlfDocument.Load(xlfPath);
            }
            else
            {
                xlfDocument = null;
            }

            /**/

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument));
            List<XmlNode> transUnitNodes = XmlNodeListToList(TransUnitNodes(xlfDocument));

            Dictionary<int, Dictionary<string, string>> charLimitDictionary = CharLimitDictionary(formattingNodes);
            AddCharLimits(transUnitNodes, charLimitDictionary[formattingNodeId]);

            Assert.AreEqual(expectedOutcome, xlfDocument.SelectNodes("//*[@size-unit='char']").Count);

        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\skeleton.skl", @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\content.xlf", "test_1.xlf", 157)]
        [DataTestMethod]
        public void AddCharLimitsContentXliff_Test(string sklPath, string xlfPath, string testFileName, int expectedOutcome)
        {
            XmlDocument sklDocument = new XmlDocument();
            sklDocument.PreserveWhitespace = true;

            if (sklPath != String.Empty)
            {
                sklDocument.Load(sklPath);
            }
            else
            {
                sklDocument = null;
            }

            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.PreserveWhitespace = true;

            if (xlfPath != String.Empty)
            {
                xlfDocument.Load(xlfPath);
            }
            else
            {
                xlfDocument = null;
            }

            /**/

            AddCharLimitsContentXliff(xlfDocument, sklDocument);
            Assert.AreEqual(expectedOutcome, xlfDocument.SelectNodes("//*[@size-unit='char']").Count);

            xlfDocument.Save(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\" + testFileName);
        }

        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\Multilingual XLSX\test.xlz")]
        [DataTestMethod]
        public void ProcessXlzFile_Test(string xlzPath)
        {

            ProcessXlzFile(xlzPath);

        }
    }
}
