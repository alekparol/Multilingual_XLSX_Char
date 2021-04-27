using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class Group : XliffStructuralElement
    {

        /* Fields */

        List<XliffStructuralElement> sChildNodes;

        /* Properties */

        public List<XliffStructuralElement> ChildNodes
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

            if (!sName.Equals("group"))
            {
                throw new Exception(String.Format("XmlNode name is different than group."));
            }

            sChildNodes = new List<XliffStructuralElement>(xNode.ChildNodes.Cast<XliffStructuralElement>());
        }

    }
}
