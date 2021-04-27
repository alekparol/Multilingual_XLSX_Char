using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class StructuralElement
    {

        /* Fields */

        protected XmlNode xNode;

        protected string sName;

        /* Properties */

        public XmlNode XmlNode
        {
            get
            {
                return xNode;
            }
        }

        public StructuralElement ParentNode
        {
            get
            {
                StructuralElement sParentNode = new StructuralElement(xNode.ParentNode);

                return sParentNode;
            }
        }

        public List<StructuralElement> ElementsPreceding
        {
            get
            {
                List<StructuralElement> nElementList = new List<StructuralElement>(xNode
                                                                                         .SelectNodes("./preceding-sibling::*")
                                                                                         .Cast<XmlNode>()
                                                                                         .Select(x => new StructuralElement(x)));
                return nElementList;
            }          
        }

        public List<StructuralElement> ElementsPrecedingSameName
        {
            get
            {
                return ElementsPrecedingTagName(Name);
            }
        }

        public List<StructuralElement> ElementsFollowing
        {
            get
            {
                List<StructuralElement> nElementList = new List<StructuralElement>(xNode
                                                                                         .SelectNodes("./following-sibling::*")
                                                                                         .Cast<XmlNode>()
                                                                                         .Select(x => new StructuralElement(x)));
                return nElementList;
            }
        }

        public List<StructuralElement> ElementsFollowingSameName
        {
            get
            {
                return ElementsFollowingTagName(Name);
            }
        }

        public XmlAttributeCollection Attributes
        {
            get
            {
                return xNode.Attributes;
            }
        }

        public string Name
        {
            get
            {
                return sName;
            }
        }  

        /* Methods */

        public void AppendAttributeValue(string attributeName, string attributeValue)
        {

            XmlAttribute xAttribute = xNode.OwnerDocument.CreateAttribute(attributeName);
            xAttribute.Value = attributeValue;

            xNode.Attributes.Append(xAttribute);

        }

        public List<StructuralElement> ElementsPrecedingTagName(string tagName)
        {
            return ElementsPreceding.FindAll(x => x.Name == tagName);
        }

        public List<StructuralElement> ElementsFollowingTagName(string tagName)
        {
            return ElementsFollowing.FindAll(x => x.Name == tagName);
        }

        /* Constructors */

        public StructuralElement()
        {

        }

        public StructuralElement(XmlNode xmlNode)
        {

            xNode = xmlNode;

            if (xmlNode != null)
            {
                sName = xmlNode.Name;

                if (sName == String.Empty)
                {
                    throw new Exception(String.Format("Creation of a new XElement object cannot be done. The XmlNode Name is empty."));
                }
            }
            else
            {
                throw new Exception(String.Format("Creation of a new XElement object cannot be done. The XmlNode is null."));
            }
        }
    }
}
