using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Multilingual_XLSX
{
    public class SKL_Manipulation
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
         * Skl Check
         */
        public static int IsSkl(XmlDocument sklDocument)
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
         * Skl Validity Check
         */

        public static int IsSklValid(XmlDocument sklDocument)
        {
            if (IsSkl(sklDocument) == 1)
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
         * Get Skl Attribute Value
         */

        public static string SklAttributeValue(XmlDocument sklDocument, string attributeName)
        {
            if (IsSklValid(sklDocument) == 1)
            {

                string sklVersion = String.Empty;

                XmlNode sklNode = sklDocument.GetElementsByTagName("tt-xliff-skl").Item(0);
                XmlAttribute sklVersionAttribute = sklNode.Attributes[attributeName];

                if (sklVersionAttribute != null)
                {
                    sklVersion = sklVersionAttribute.Value;
                }

                return sklVersion;
            }
            else
            {
                return String.Empty;
            }
        }

        /*
         * Get Skl Version
         */

        public static string SklVersion(XmlDocument sklDocument)
        {
            return SklAttributeValue(sklDocument, "version");
        }


        /*
         * skeleton.skl Check
         */
        public static bool IsSkeleton(XmlDocument sklDocument)
        {
            if (SklVersion(sklDocument) != String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
         * Return All Formatting Nodes
         */
        static public XmlNodeList GetAllFormattingNodes(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkl(sklDocument) == 1)
            {
                transUnitList = sklDocument.GetElementsByTagName("formatting");
            }

            return transUnitList;
        }

        /*
         * Return All Formatting Nodes Containing Max.N.Char
         */
        static public XmlNodeList GetFormattingNodesContainingMaxChar(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkl(sklDocument) == 1)
            {
                transUnitList = sklDocument.SelectNodes("//formatting[contains(text(), \"max.\") and contains(text(),\".char\")]");
            }

            return transUnitList;
        }

        /*
        * 
        */
        static public XmlNodeList GetAllPlaceholderNodes(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkeleton(sklDocument))
            {
                transUnitList = sklDocument.GetElementsByTagName("tu-placeholder");
            }

            return transUnitList;
        }

        /*
         * To be continued
         */

        static public List<XmlNode> GetAllPlaceholderNodesPreceedingFormattingNode(XmlNode formattingNode)
        {
            List<XmlNode> transUnitList = new List<XmlNode>();
            XmlNode previousSibling = formattingNode.PreviousSibling;

            while(previousSibling.Name == "tu-placeholder")
            {
                transUnitList.Add(previousSibling);
            }

            return transUnitList;
        }

        


        static public string GetCharLimit(XmlNode formattingNode)
        {
            Regex regex = new Regex("max\\.\\d +\\.char");
            string charLimit = String.Empty;

            if (formattingNode.Name == "formatting")
            {
                MatchCollection charLimitMatches = regex.Matches(formattingNode.InnerText);
                if (charLimitMatches.Count == 1) charLimit = charLimitMatches[0].Value;
            }

            return charLimit;

        }

        //static public XmlNodeList FilterFormattingNodesByMaxChar(XmlNodeList formattingNodeList)

    }
}
