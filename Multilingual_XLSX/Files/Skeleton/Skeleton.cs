using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Multilingual_XLSX
{
    public class Skeleton : StructuralFile
    {

        /* Fields */

        /* Properties */
        public string SkeletonVersion
        {
            get
            {
                return xRootNode.Attributes["version"].Value;
            }
        }

        public List<Formatting> Formattings
        {
            get
            {
                List<Formatting> nElementList = new List<Formatting>(StructuralElements
                                                   .FindAll(x => x.Name == "formatting")
                                                   .Cast<Formatting>());
                return nElementList;
            }
        }

        public List<Formatting> FormattingsWCharLimit
        {
            get
            {
                return Formattings.FindAll(x => x.CharLimit != String.Empty);
            }
        }

        public List<TuPlaceholder> TuPlaceholders
        {
            get
            {
                List<TuPlaceholder> nElementList = new List<TuPlaceholder>(StructuralElements
                                                   .FindAll(x => x.Name == "tu-placeholder")
                                                   .Cast<TuPlaceholder>());
                return nElementList;
            }
        }

        /* Methods */

        public bool IsSkeletonSkl()
        {
            if (SkeletonVersion != String.Empty)
            {
                return true;
            }

            return false;
        }

        /* Constructors */

        public Skeleton()
        {

        }

        public Skeleton(XmlDocument xmlDocument) : base(xmlDocument)
        {
            if (!xDocumentType.Name.Equals("tt-xliff-skl"))
            {
                throw new Exception(String.Format("The type of the file provided is not equal to Skeleton."));
            }
        }
    }
}
