using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class Xliff
    {

        /* Fields */

        private string sXliff;
        private XmlDocument xXliff;

        private XmlNode nXliff;
        private XmlNodeList nStructuralElements;

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

        public List<StructuralElement> StructuralElements
        {
            get
            {
                List<StructuralElement> nElementList = new List<StructuralElement>(nStructuralElements.Cast<StructuralElement>());
                return nElementList;
            }
        }

        public List<TransUnit> TransUnits
        {
            get
            {
                List<TransUnit> nElementList = new List<TransUnit>(StructuralElements
                                                   .FindAll(x => x.Name == "trans-unit")
                                                   .Cast<TransUnit>());
                return nElementList;
            }
        }

        public List<TransUnit> TranslatableElements
        {
            get
            {
                return TransUnits.FindAll(x => x.Translate == "yes");
            }
        }

        public List<TransUnit> UntranslatableTransUnits
        {
            get
            {
                return TransUnits.FindAll(x => x.Translate == "no");
            }
        }

        public List<TransUnit> Groups
        {
            get
            {
                List<TransUnit> nElementList = new List<TransUnit>(StructuralElements
                                                   .FindAll(x => x.Name == "group")
                                                   .Cast<TransUnit>());
                return nElementList;
            }
        }

        /* Methods */

        public bool IsContentXliff()
        {
            if (XliffVersion != String.Empty && XliffXmlns != String.Empty)
            {
                return true;
            }

            return false;
        }

        public List<StructuralElement> GetNodesByTagName(string tagName)
        {

            List<StructuralElement> nodesList = new List<StructuralElement>(xXliff
                                                       .GetElementsByTagName(tagName)
                                                       .Cast<StructuralElement>());
            return nodesList;
        }

        public List<StructuralElement> GetNodesByTagNameAttributeValue(string tagName, string attributeName, string attributeValue)
        {

            List<StructuralElement> nodeList = GetNodesByTagName(tagName)
                                     .FindAll(x => x.Attributes[attributeName]
                                                    .Value == attributeValue);
            return nodeList;
        }


        public void GroupNodes(List<StructuralElement> nodeList)
        {
            if (nodeList.Count > 0)
            {
                XmlNode firstNode = nodeList.First().XmlNode;
                XmlNode parentNode = firstNode.ParentNode;

                XmlElement groupNode = parentNode.OwnerDocument.CreateElement("group");
                parentNode.InsertBefore(groupNode, firstNode); // Append group Node Before First Node from the List

                foreach (StructuralElement transUnitNode in nodeList)
                {
                    parentNode.RemoveChild(transUnitNode.XmlNode);
                    groupNode.AppendChild(transUnitNode.XmlNode);
                }
            }
        }

        /* Constructors */

        public Xliff()
        {

        }

        public Xliff(XmlDocument xmlDocument)
        {

            xXliff = xmlDocument; 

            if (xmlDocument != null)
            {
                if (xmlDocument.DocumentType.Name.Equals("xliff"))
                {
                    
                    XmlNodeList nXliffList = xmlDocument.GetElementsByTagName("xliff");

                    if (nXliffList.Count == 1)
                    {

                        nXliff = nXliffList.Item(0);
                        nStructuralElements = xmlDocument.SelectNodes("//body/*[self::trans-unit or self::group]");

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
                throw new Exception(String.Format("Creation of a new Xliff object cannot be done. The XmlDocument is null."));
            }
        }

    }
}
