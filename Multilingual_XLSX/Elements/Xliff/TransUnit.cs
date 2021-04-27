using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class TransUnit : XliffStructuralElement
    {

        /* Fields */

        /* Properties */

        public string Id
        {
            get
            {
                if (xNode.Attributes["id"] != null)
                {
                    return xNode.Attributes["id"].Value;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string Translate
        {
            get
            {
                if (xNode.Attributes["translate"] != null)
                {
                    return xNode.Attributes["translate"].Value;
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        /* Methods */

        /* Constructors */

        public TransUnit()
        {

        }

        public TransUnit(XmlNode xmlNode) : base(xmlNode)
        {
            if (! sName.Equals("trans-unit"))
            {
                throw new Exception(String.Format("XmlNode name is different than trans-unit."));
            }
        }

    }
}
