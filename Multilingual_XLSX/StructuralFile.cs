using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class StructuralFile
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

        /* Methods */

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
