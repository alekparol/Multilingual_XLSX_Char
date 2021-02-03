﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
         * 
         */
        static public XmlNodeList GetFormattingNodesContainingMaxChar(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkeletonSkl(sklDocument))
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

            if (IsSkeletonSkl(sklDocument))
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
                charLimit = charLimitMatches[0].Value;
            }

            return charLimit;

        }

        //static public XmlNodeList FilterFormattingNodesByMaxChar(XmlNodeList formattingNodeList)

    }
}
