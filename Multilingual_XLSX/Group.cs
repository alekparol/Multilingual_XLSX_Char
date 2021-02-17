using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class Group : StructuralElement
    {

        /* Fields */

        List<StructuralElement> sChildNodes;

        /* Properties */

        public List<StructuralElement> ChildNodes
        {
            get
            {              
                return sChildNodes;
            }
        }

        /* Methods */

        /* Constructors */

        public Group()
        {

        }

        public Group(XmlNode xmlNode) : base(xmlNode)
        {
            List<StructuralElement> childNodes = new List<StructuralElement>(xNode.ChildNodes.Cast<StructuralElement>());
        }

    }
}
