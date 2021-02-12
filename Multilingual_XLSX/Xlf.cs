using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    class Xlf
    {

        /* Fields */

        private string sXlf;
        private XmlDocument xXlf;

        private XmlNode nXliff;
        private XmlNodeList nTransUnit;

        /* Properties */

        public string XliffVersion
        {
            get
            {
                return nXliff.Attributes["version"].Value;
            }
        }

        public string XliffXmlns
        {
            get
            {
                return nXliff.Attributes["xmlns:logoport"].Value;
            }
        }

        public List<XmlNode> TransUnitNodes
        {
            get
            {
                List<XmlNode> nTransUnitList = new List<XmlNode(nTransUnit.Cast<XmlNode>());
                return nTransUnitList;
            }
        }

        public List<XmlNode> TranslatableTransUnitNodes
        {
            get
            {
                return TransUnitNodes.FindAll(x => x.Attributes["translate"].Value == "yes");                       
            }
        }

        public List<XmlNode> UntranslatableTransUnitNodes
        {
            get
            {
                return TransUnitNodes.FindAll(x => x.Attributes["translate"].Value == "no");
            }
        }


        /* Methods */

        public bool IsContentXlf()
        {
            if (XliffVersion != String.Empty && XliffXmlns != String.Empty)
            {
                return true;
            }

            return false;
        }

        public List<XmlNode> GetNodesByTagName(XmlDocument xlfDocument, string tagName)
        {

            List<XmlNode> nodesList = new List<XmlNode>(xlfDocument
                                                       .GetElementsByTagName(tagName)
                                                       .Cast<XmlNode>());
            return nodesList;
        }

        public List<XmlNode> GetNodesByTagNameAttributeValue(XmlDocument xlfDocument, string tagName, string attributeName, string attributeValue)
        {

            string xpathTagName = "//" + tagName;
            string xpathAttributeValue = "[@" + attributeName + "=\'" + attributeValue + "\']";

            string fullXPath = xpathTagName + xpathAttributeValue;

            List<XmlNode> nodeList = new List<XmlNode>(xlfDocument
                                                       .SelectNodes(fullXPath)
                                                       .Cast<XmlNode>());
            return nodeList;
        }

        /* Constructors */

        public Xlf()
        {

        }

        public Xlf(XmlDocument xmlDocument)
        {

            xXlf = xmlDocument; 

            if (xmlDocument != null)
            {
                if (xmlDocument.DocumentType.Name.Equals("xliff"))
                {
                    
                    XmlNodeList nXliffList = xmlDocument.GetElementsByTagName("xliff");

                    if (nXliffList.Count == 1)
                    {

                        nXliff = nXliffList.Item(0);
                        nTransUnit = xmlDocument.GetElementsByTagName("tran-unit");

                    }
                    else
                    {
                        throw new Exception(String.Format("The file contains multiple xliff nodes."));
                    }
                }
                else
                {
                    throw new Exception(String.Format("The type of the file provided is not equal to Xliff."));
                }
            }
            else
            {
                throw new Exception(String.Format("Creation of a new Xlf object cannot be done. The XmlDocument is null."));
            }
        }

    }
}
