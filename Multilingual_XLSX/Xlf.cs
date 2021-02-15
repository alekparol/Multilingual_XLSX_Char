using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class Xlf
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

        public List<XmlNode> TransUnits
        {
            get
            {
                List<XmlNode> nTransUnitList = new List<XmlNode>(nTransUnit.Cast<XmlNode>());
                return nTransUnitList;
            }
        }

        public List<XmlNode> TranslatableTransUnits
        {
            get
            {
                return TransUnits.FindAll(x => x.Attributes["translate"].Value == "yes");                       
            }
        }

        public List<XmlNode> UntranslatableTransUnits
        {
            get
            {
                return TransUnits.FindAll(x => x.Attributes["translate"].Value == "no");
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

        public List<XmlNode> GetNodesByTagName(string tagName)
        {

            List<XmlNode> nodesList = new List<XmlNode>(xXlf
                                                       .GetElementsByTagName(tagName)
                                                       .Cast<XmlNode>());
            return nodesList;
        }

        public List<XmlNode> GetNodesByTagNameAttributeValue(string tagName, string attributeName, string attributeValue)
        {

            List<XmlNode> nodeList = GetNodesByTagName(tagName)
                                     .FindAll(x => x.Attributes[attributeName]
                                                    .Value == attributeValue);
            return nodeList;
        }

        public void GroupNodes(List<XmlNode> nodeList)
        {
            if (nodeList.Count > 0)
            {
                XmlNode firstNode = nodeList.First();
                XmlNode parentNode = firstNode.ParentNode;

                XmlElement groupNode = parentNode.OwnerDocument.CreateElement("group");
                parentNode.InsertBefore(groupNode, firstNode); // Append group Node Before First Node from the List

                foreach (XmlNode transUnitNode in nodeList)
                {
                    parentNode.RemoveChild(transUnitNode);
                    groupNode.AppendChild(transUnitNode);
                }
            }
        }

        /*public void AddAttributeAndValue(List<XmlNode> transUnitNodeList, string attributeName, string attributeValue)
        {

            XmlAttribute unitSize = transUnitNode.OwnerDocument.CreateAttribute(attributeName);
            unitSize.Value = attributeValue;

            transUnitNode.Attributes.Append(unitSize);

        }*/

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
