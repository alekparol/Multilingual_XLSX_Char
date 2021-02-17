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

        private string sStructuralFile;
        private XmlDocument xStructuralFile;

        private XmlDocumentType xDocumentType;
        private XmlNode xRootNode;

        private XmlNode xBodyNode;
        private XmlNodeList xStructuralElements;

        /* Properties */

        public List<StructuralElement> StructuralElements
        {
            get
            {
                List<StructuralElement> nElementList = new List<StructuralElement>(xStructuralElements.Cast<StructuralElement>());
                return nElementList;
            }
        }

        /* Methods */

        public List<StructuralElement> GetNodesByTagName(string tagName)
        {

            List<StructuralElement> nodesList = new List<StructuralElement>(xStructuralFile
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

        public StructuralFile()
        {

        }

        public StructuralFile(XmlDocument xmlDocument)
        {

            xStructuralFile = xmlDocument;

            if (xStructuralFile != null)
            {
                xDocumentType = xStructuralFile.DocumentType;

                if (xDocumentType == null)
                {
                    throw new Exception(String.Format("The file does not contain document type."));
                }

                xRootNode = xStructuralFile.DocumentElement;            

                if (xRootNode == null)
                {
                    throw new Exception(String.Format("The file does not contain root node."));
                }

                XmlNodeList xBodyNodes = xRootNode.SelectNodes(".//body");

                if (xBodyNodes.Count == 1)
                {
                    xBodyNode = xBodyNodes.Item(0);
                    xStructuralElements = xBodyNode.ChildNodes;
                }
                else
                {
                    throw new Exception(String.Format("The file contains {0} number of body nodes.", xBodyNodes.Count));
                }
            }
            else
            {
                throw new Exception(String.Format("Creation of a new Xliff object cannot be done. The XmlDocument is null."));
            }
        }
    }
}
