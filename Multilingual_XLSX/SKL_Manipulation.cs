using System;
using System.Collections.Generic;
using System.Linq;
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
         * Return All Nodes Specified By Name
         */
        static public XmlNodeList NodesSkl(XmlDocument sklDocument, string tagName)
        {

            XmlNodeList transUnitList = null;

            if (IsSkl(sklDocument) == 1)
            {
                transUnitList = sklDocument.GetElementsByTagName(tagName);
            }

            return transUnitList;
        }

        /*
         * Return All Formatting Nodes
         */
        static public XmlNodeList FormattingNodes(XmlDocument sklDocument)
        {
            return NodesSkl(sklDocument, "formatting");
        }

        /*
         * Return All Formatting Nodes Containing Max.N.Char
         */
        static public XmlNodeList FormattingNodesCharLimit(XmlDocument sklDocument)
        {

            XmlNodeList transUnitList = null;

            if (IsSkl(sklDocument) == 1)
            {
                transUnitList = sklDocument.SelectNodes("//formatting[contains(text(), \"max.\") and contains(text(),\".char\")]");
            }

            return transUnitList;
        }

        /*
        * Return All tu-placeholder Node
        */
        static public XmlNodeList PlaceholderNodes(XmlDocument sklDocument)
        {
            return NodesSkl(sklDocument, "tu-placeholder");
        }

        /*
         * Return All Nodes Specified by Name Preceding a Passed Node
         */
        static public List<XmlNode> NodesPreceding(XmlNode sklNode, string tagName)
        {
            List<XmlNode> nodesPrecedingList = new List<XmlNode>();
            XmlNodeList nodesPreceding = sklNode.SelectNodes("./preceding-sibling::" + tagName);

            if (nodesPreceding.Count > 0)
            {
                foreach (XmlNode nodePreceding in nodesPreceding)
                {
                    nodesPrecedingList.Add(nodePreceding);
                }
            }

            return nodesPrecedingList;
        }

        /*
         * Return All tu-placeholder Nodes Preceding Formatting Node
         */

        static public List<XmlNode> PlaceholderNodesPreceding(XmlNode formattingNode)
        {
            return NodesPreceding(formattingNode, "tu-placeholder");
        }

        /*
         * Return All formatting Nodes Preceding Formatting Node
         */

        static public List<XmlNode> FormattingNodesPreceding(XmlNode formattingNode)
        {
            return NodesPreceding(formattingNode, "formatting");
        }

        /*
         * Returns tu-placeholder Nodes Preceding Directly Formatting Node
         */

        static public List<XmlNode> PlaceholderNodesPrecedingDirectly(XmlNode formattingNode)
        {
            List<XmlNode> placeholderNodesPreceding = PlaceholderNodesPreceding(formattingNode);
            List<XmlNode> formattingNodesPreceding = FormattingNodesPreceding(formattingNode);

            int formattingNodesCount = formattingNodesPreceding.Count;

            if (formattingNodesCount > 0)
            {

                XmlNode formattingNodePreceding = formattingNodesPreceding[formattingNodesCount - 1];
                List<XmlNode> placeholderNodesPrecedingPrevious = PlaceholderNodesPreceding(formattingNodePreceding);

                placeholderNodesPreceding = placeholderNodesPreceding.FindAll(x => placeholderNodesPrecedingPrevious.Contains(x) == false);

            }

                return placeholderNodesPreceding;

        }

        /*
         * Returns the String with Char Limit Size
         */

        static public string GetCharLimit(XmlNode formattingNode)
        {
            Regex regex = new Regex("max\\.(\\d+)\\.char");
            string charLimit = String.Empty;

            if (formattingNode.Name == "formatting")
            {
                MatchCollection charLimitMatches = regex.Matches(formattingNode.InnerText);
                if (charLimitMatches.Count > 0) charLimit = charLimitMatches[0].Groups[1].Value;
            }

            return charLimit;

        }

    }
}
