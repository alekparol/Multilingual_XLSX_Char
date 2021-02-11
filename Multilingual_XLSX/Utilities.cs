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

        static public Dictionary<string, string> GetCharLimitDictionary (List<XmlNode> formattingNodeList)
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

        /* TODO: Add Functionality to make char limit in the groups */

    }
}
