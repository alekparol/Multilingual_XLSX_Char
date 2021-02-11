using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static Multilingual_XLSX.SKL_Manipulation;
using static Multilingual_XLSX.XLF_Manipulation;

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
         * 
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

        static public Dictionary<string, Dictionary<string, string>> GetCharLimitDictionary2(List<XmlNode> formattingNodeList)
        {

            Dictionary<string, Dictionary<string, string>> charLimitDictionary = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> partialCharLimit = new Dictionary<string, string>();
            List<XmlNode> placeholderList = new List<XmlNode>();

            if (formattingNodeList.Count > 0)
            {
                for (int i = 0; i < formattingNodeList.Count; i++)
                {
                    List<XmlNode> secondList = formattingNodeList.GetRange(i, 1);
                    /*XmlNode formattingNode = formattingNodeList[i];
                    partialCharLimit = new Dictionary<string, string>();

                    string charLimit = GetCharLimit(formattingNode);

                    placeholderList = PlaceholderNodesPrecedingDirectly(formattingNode);

                    foreach (XmlNode placeholderNode in placeholderList)
                    {
                        partialCharLimit.Add(placeholderNode.Attributes["id"].Value, charLimit);
                    }*/
                    partialCharLimit = GetCharLimitDictionary(secondList);
                    charLimitDictionary.Add(i.ToString(), partialCharLimit);

                }

            }

            return charLimitDictionary;

        }

        /*
         * 
         */

        static public Dictionary<string, string> GetCharLimitDictionary(List<XmlNode> formattingNodeList)
        {

            Dictionary<string, string> charLimitDictionary = new Dictionary<string, string>();
            List<XmlNode> placeholderList = new List<XmlNode>();

            if (formattingNodeList.Count > 0)
            {
                foreach (XmlNode formattingNode in formattingNodeList)
                {
                    string charLimit = GetCharLimit(formattingNode);

                    placeholderList = PlaceholderNodesPrecedingDirectly(formattingNode);

                    foreach (XmlNode placeholderNode in placeholderList)
                    {
                        charLimitDictionary.Add(placeholderNode.Attributes["id"].Value, charLimit);
                    }

                }

            }

            return charLimitDictionary;

        }

        /*
         * 
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
                List<string> nodeIds = new List<string>();
                string charLimitValue = "";

                foreach (string dictionaryKey in charLimitDictionary.Keys)
                {
                    nodeIds.Add(dictionaryKey);
                }

                if (nodeIds.Count > 0)
                {
                    charLimitValue = charLimitDictionary[nodeIds[0]];
                }

                List<XmlNode> foundNodes = transUnitNodes.FindAll(x => nodeIds.Contains(x.Attributes["id"].Value));

                if (foundNodes.Count > 1)
                {
                    GroupNodes(foundNodes);

                    XmlNode groupNode = foundNodes[0].OwnerDocument.SelectSingleNode("//group");
                    AddCharLimit(groupNode, charLimitValue);

                }
            }
        }

        /*static public Dictionary<string, string> GetCharLimitDictionary2(List<XmlNode> formattingNodeList)
        {

            Dictionary<string, string> charLimitDictionary = new Dictionary<string, string>();
            List<XmlNode> placeholderList = new List<XmlNode>();

            if (formattingNodeList.Count > 0)
            {
                foreach (XmlNode formattingNode in formattingNodeList)
                {
                    string charLimit = GetCharLimit(formattingNode);

                    placeholderList = PlaceholderNodesPrecedingDirectly(formattingNode);

                    foreach (XmlNode placeholderNode in placeholderList)
                    {
                        //string arg = formattingNodeList.FindIndex(x => x.Equals(formattingNode)).ToString() + "," + placeholderNode.Attributes["id"].Value;
                        charLimitDictionary.Add(formattingNodeList.FindIndex(x => x.Equals(formattingNode)).ToString() + "," + placeholderNode.Attributes["id"].Value, charLimit);
                    }

                }

            }

            return charLimitDictionary;

        }*/


        static public void AddCharLimits(XmlDocument contentXlf, Dictionary<string, string> charLimitDictionary)
        {

            XmlNodeList translatableNodes = TransUnitTranslatableNodes(contentXlf);

            foreach(XmlNode translatableNode in translatableNodes)
            {

                string transUnitId = translatableNode.Attributes["id"].Value;

                if (charLimitDictionary.ContainsKey(transUnitId))
                {
                    AddCharLimit(translatableNode, charLimitDictionary[transUnitId]);
                }
            }

        }

        static public void AddCharLimits2(XmlDocument contentXlf, Dictionary<string, Dictionary<string, string>> charLimitDictionary)
        {

            XmlNodeList translatableNodes = TransUnitTranslatableNodes(contentXlf);
            List<XmlNode> translatableNodesList = new List<XmlNode>();
            List<XmlNode> subList = new List<XmlNode>();

            foreach (XmlNode el in translatableNodes)
            {
                translatableNodesList.Add(el);
            }

            foreach (string dicKey in charLimitDictionary.Keys)
            {
                if (charLimitDictionary[dicKey].Count == 1)
                {
                    foreach (string dic2Key in charLimitDictionary[dicKey].Keys)
                    {
                        XmlNode thisNode = translatableNodesList.Find(x => x.Attributes["id"].Value == dic2Key);
                        AddCharLimit(thisNode, charLimitDictionary[dicKey][dic2Key]);
                    }
                }
                else
                {
                    string charLimit = "";
                    foreach (string dic2Key in charLimitDictionary[dicKey].Keys)
                    {
                        XmlNode thisNode = translatableNodesList.Find(x => x.Attributes["id"].Value == dic2Key);
                        subList.Add(thisNode);
                        charLimit = charLimitDictionary[dicKey][dic2Key];
                    }

                    GroupNodes(subList);

                    XmlNode group = contentXlf.SelectSingleNode("//group");
                    AddCharLimit(group, charLimit);

                }
            }

        }

        /*static public void AddCharLimitsDictionary(XmlDocument contentXlf, Dictionary<string, string> charLimitDictionary)
        {
            if (charLimitDictionary.Count > 0)
            {
                if (charLimitDictionary.Count == 1)
                {
                    AddCharLimitsSingle(contentXlf, charLimitDictionary);
                }
                else
                {
                    AddCharLimitsMultiple(contentXlf, charLimitDictionary);
                }
            }
        }*/

        /* TODO: Add Functionality to make char limit in the groups */

    }
}
