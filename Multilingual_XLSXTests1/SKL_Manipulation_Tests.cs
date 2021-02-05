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
        [DataRow()]
        [DataTestMethod]
        public void IsNull_Test(string filePath, bool expectedOutcome)
        {

            XmlDocument sklDocument = new XmlDocument();
            sklDocument.Load(filePath);



            Assert.AreEqual(expectedOutcome, IsNull(sklDocument));
        }
    }
}