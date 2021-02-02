using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Multilingual_XLSX
{
    class SKL_Manipulation
    {

        /*
         * Null Check
         */

        public static bool IsNull(XmlDocument sklDocument)
        {
            if (sklDocument == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Xliff Check
         */
        public static int IsSkeleton(XmlDocument sklDocument)
        {
            if (!IsNull(sklDocument))
            {
                if (sklDocument.DocumentType.Name.Equals("tt-xliff-skl"))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        /*
         * Xliff Smoke Chekc
         */

        public static int IsSkeletonValid(XmlDocument sklDocument)
        {
            if (IsSkeleton(sklDocument) == 1)
            {
                XmlNodeList skeletonNodes = sklDocument.GetElementsByTagName("tt-xliff-skl");

                if (skeletonNodes.Count == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        /*
         * Get Xliff Version
         */

        public static string SkeletonVersion(XmlDocument sklDocument)
        {
            if (IsSkeletonValid(sklDocument) == 1)
            {
                XmlNode xliffNode = sklDocument.GetElementsByTagName("tt-xliff-skl").Item(0);
                string xliffVersion = xliffNode.Attributes["version"].Value;

                return xliffVersion;
            }
            else
            {
                return String.Empty;
            }
        }


        /*
         * content.xlf Check
         */
        public static bool IsSkeletonSkl(XmlDocument sklDocument)
        {
            if (SkeletonVersion(sklDocument) != String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
         * 
         */
        static public XmlNodeList GetAllFormattingNodes(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkeletonSkl(sklDocument))
            {
                transUnitList = sklDocument.GetElementsByTagName("formatting");
            }

            return transUnitList;
        }

        /*
         * To be continued
         */

    }
}
