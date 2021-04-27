using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class Xliff : StructuralFile
    {

        /* Fields */

        /* Properties */

        public string XliffVersion
        {
            get
            {
                return xRootNode.Attributes["version"].Value;
            }
        }

        public string XliffXmlns
        {
            get
            {
                return xRootNode.Attributes["xmlns:logoport"].Value;
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

        public List<TransUnit> TranslatableTransUnits
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

        public List<Group> Groups
        {
            get
            {
                List<Group> nElementList = new List<Group>(StructuralElements
                                                   .FindAll(x => x.Name == "group")
                                                   .Cast<Group>());
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

        public void GroupNodes(List<XliffStructuralElement> nodeList)
        {
            if (nodeList.Count > 0)
            {
                XmlNode firstNode = nodeList.First().XmlNode;
                XmlNode parentNode = firstNode.ParentNode;

                XmlElement groupNode = parentNode.OwnerDocument.CreateElement("group");
                parentNode.InsertBefore(groupNode, firstNode); // Append group Node Before First Node from the List

                foreach (XliffStructuralElement transUnitNode in nodeList)
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

        public Xliff(XmlDocument xmlDocument) : base(xmlDocument)
        {
            if (! xDocumentType.Name.Equals("xliff"))
            {
                throw new Exception(String.Format("The type of the file provided is not equal to Xliff."));
            }
        }
    }
}
