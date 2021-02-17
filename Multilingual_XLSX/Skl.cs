using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class Skl
    {

        /* Fields */

        private string sSkl;
        private XmlDocument xSkl;

        private XmlNode nSkl;
        private XmlNodeList nElements;

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

        public List<XElement> Elements
        {
            get
            {
                List<XElement> nElementList = new List<XElement>(nElements.Cast<XElement>());
                return nElementList;
            }
        }

        public List<XElement> TranslatableElements
        {
            get
            {
                return Elements.FindAll(x => x.Translate == "yes");
            }
        }

        public List<XElement> UntranslatableTransUnits
        {
            get
            {
                return Elements.FindAll(x => x.Translate == "no");
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

        public List<XElement> GetNodesByTagName(string tagName)
        {

            List<XElement> nodesList = new List<XElement>(xXlf
                                                       .GetElementsByTagName(tagName)
                                                       .Cast<XElement>());
            return nodesList;
        }


        public List<XElement> GetNodesByTagNameAttributeValue(string tagName, string attributeName, string attributeValue)
        {

            List<XElement> nodeList = GetNodesByTagName(tagName)
                                     .FindAll(x => x.Attributes[attributeName]
                                                    .Value == attributeValue);
            return nodeList;
        }
        
        /* Constructors */

        public Skl()
        {

        }

        public Skl(XmlDocument xmlDocument)
        {

            xSkl = xmlDocument;

            if (xmlDocument != null)
            {
                if (xmlDocument.DocumentType.Name.Equals("tt-xliff-skl"))
                {

                    XmlNodeList nSklList = xmlDocument.GetElementsByTagName("tt-xliff-skl");

                    if (nSklList.Count == 1)
                    {

                        nSkl = nSklList.Item(0);
                        nElements = xmlDocument.GetElementsByTagName("tran-unit");

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
