using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static Multilingual_XLSX.SKL_Manipulation;
using static Multilingual_XLSX.XLF_Manipulation;
using static Multilingual_XLSX.XLZ_Manipulation;

/*
         * What do we need? 
         * 
         * 1) Open XLZ file.
         * 2) Open content.xlf file.
         *    2.1) Get all the <trans-unit> elements (or all trans-unit with translation set to "yes").
         * 3) Open skeleton.skl file.
         *    3.1) Get all the <formatting> elements.
         *    3.2) Get all the <tu-placeholder> elements.
         *    3.2) From the list of formatting elements, get all the elements that contain "max.\d+.char" text. 
         * 4) Function that get the formatting element and returns a list of tuples [<int,int>,...,] in which first of the tuple's element symbolizes id of the trans-unit and the latter, char limit. [HERE NEEDS TO BE ADDED OUR ADDITION]
         * 5) For each formatting element from the list 3.2) use function from 4) and create a super list containing them all. 
         * 6) Function that gets int argument and trans-unit and adds int argument as a char limit. [WHOLE CLASS OF STATIC METHODS WOULD BE GREAT]
         * 7) For each element of a dictionary find trans-unit node of a given id and then use the function from 6). 
         * 8) Save the changes and update the XLZ file. 
         * 
         * 
         * NOTE: Instead of list of tuples it would be better to use a dictionary. 
         * 
         */

namespace Multilingual_XLSX
{
    public class Utilities
    {

        /*
         * Converts XmlNodeList to List<XmlNode>
         */
        static public List<XmlNode> XmlNodeListToList(XmlNodeList xmlNodeList)
        {
            List<XmlNode> convertedXmlNodeList = new List<XmlNode>();

            if (xmlNodeList != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    convertedXmlNodeList.Add(xmlNode);
                }
            }

            return convertedXmlNodeList;
            
        }

        /*
         * Returns Dictionary<string, string> Containing placeholder Node's IDs as Keys and Char Limit as Value
         * 
         */
        static public Dictionary<string, string> CharLimitDictionaryPerFormatting(XmlNode formattingNode)
        {

            Dictionary<string, string> charLimitDictionary = new Dictionary<string, string>();
            List<XmlNode> precedingPlaceholderNodes = PlaceholderNodesPrecedingDirectly(formattingNode);

            string charLimit = GetCharLimit(formattingNode);
            
            foreach (XmlNode precedingPlaceholderNode in precedingPlaceholderNodes)
            {
                charLimitDictionary.Add(precedingPlaceholderNode.Attributes["id"].Value, charLimit);
            }

            return charLimitDictionary;

        }

        /*
         * Returns Dictionary<int, Dictionary<string, string>> Containing Formatting Node's IDs as Keys and a Dictionary<string, string> as Value
         */
        static public Dictionary<int, Dictionary<string, string>> CharLimitDictionary(List<XmlNode> formattingNodeList)
        {

            Dictionary<int, Dictionary<string, string>> charLimitDictionary = new Dictionary<int, Dictionary<string, string>>();

            if (formattingNodeList.Count > 0)
            {
                for (int i = 0; i < formattingNodeList.Count; i++)
                {

                    charLimitDictionary.Add(i, 
                                            CharLimitDictionaryPerFormatting(formattingNodeList[i]));
                }
            }

            return charLimitDictionary;

        }

        /*
         * Adds Char Limit Specific Attributes (size-unit and maxwidth) to the List<XmlNode> Passed, Using the Information About Char Limit Value for Specific XmlNode IDs from Dictionary<string, string>
         */
        static public void AddCharLimitsSingle(List<XmlNode> transUnitNodes, Dictionary<string, string> charLimitDictionary)
        {
            if (charLimitDictionary.Count == 1)
            {
                string nodeId = "";
                string charLimitValue = "";

                foreach (string dictionaryKey in charLimitDictionary.Keys)
                {
                    nodeId = dictionaryKey;
                    charLimitValue = charLimitDictionary[nodeId];
                }

                List<XmlNode> foundNodes = transUnitNodes.FindAll(x => x.Attributes["id"].Value == nodeId);

                if (foundNodes.Count == 1)
                {
                    AddCharLimit(foundNodes[0], charLimitValue);
                }
            }
        }

        /*
         * 
         */
        static public void AddCharLimitsMultiple(List<XmlNode> transUnitNodes, Dictionary<string, string> charLimitDictionary)
        {
            if (charLimitDictionary.Count > 1)
            {

                List<XmlNode> foundNodes = new List<XmlNode>();

                List<string> nodeIds = new List<string>();
                string charLimitValue = "";

                foreach (string dictionaryKey in charLimitDictionary.Keys)
                {
                    nodeIds.Add(dictionaryKey);
                    foundNodes.Add(transUnitNodes.Find(x => x.Attributes["id"].Value == dictionaryKey));
                }

                if (nodeIds.Count > 0)
                {
                    charLimitValue = charLimitDictionary[nodeIds[0]];
                }

                if (foundNodes.Count > 1)
                {
                    GroupNodes(foundNodes);

                    XmlNode groupNode = foundNodes[0].ParentNode;

                    if (groupNode.Name == "group")
                    {
                        AddCharLimit(groupNode, charLimitValue);
                    }
                }
            }
        }

        /*
         * 
         */
        static public void AddCharLimits(List<XmlNode> transUnitNodes, Dictionary<string, string> charLimitDictionary)
        {
            if (charLimitDictionary.Count > 0)
            {
                if (charLimitDictionary.Count == 1)
                {
                    AddCharLimitsSingle(transUnitNodes, charLimitDictionary);
                }
                else
                {
                    AddCharLimitsMultiple(transUnitNodes, charLimitDictionary);
                }
            }
        }

        /*
         * 
         */
        static public void AddCharLimitsContentXlf(XmlDocument xlfDocument, XmlDocument sklDocument)
        {

            List<XmlNode> formattingNodes = XmlNodeListToList(FormattingNodesCharLimit(sklDocument));
            List<XmlNode> transUnitNodes = XmlNodeListToList(TransUnitNodes(xlfDocument));

            Dictionary<int, Dictionary<string, string>> charLimitDictionary = CharLimitDictionary(formattingNodes);

            for (int i = 0; i < formattingNodes.Count; i++)
            {
                AddCharLimits(transUnitNodes, charLimitDictionary[i]);
            }

        }

        /*
         * 
         */
        static public void ProcessXlzFile(string xlzFilePath)
        {
            if (xlzFilePath != "")
            {
                string sklFile = ReadSkeletonSKL(xlzFilePath);
                string xlfFile = ReadContentXLF(xlzFilePath);

                XmlDocument sklDocument = new XmlDocument();
                sklDocument.PreserveWhitespace = true;
                sklDocument.LoadXml(sklFile);

                XmlDocument xlfDocument = new XmlDocument();
                xlfDocument.PreserveWhitespace = true;
                xlfDocument.LoadXml(xlfFile);

                AddCharLimitsContentXlf(xlfDocument, sklDocument);
                xlfFile = xlfDocument.OuterXml;

                UpdateContentXLF(xlzFilePath, xlfFile);
            }
        }
        /* TODO: Add Functionality to make char limit in the groups */

    }
}
