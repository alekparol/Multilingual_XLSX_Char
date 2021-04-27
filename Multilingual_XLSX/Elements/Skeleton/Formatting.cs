using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

namespace Multilingual_XLSX
{
    public class Formatting : StructuralElement
    {

        /* Fields */

        protected string sCharLimit;

        /* Properties */

        public List<TuPlaceholder> PlaceholderPreceding
        {
            get
            {
                List<TuPlaceholder> nElementList = new List<TuPlaceholder>(ElementsPrecedingTagName("tu-placeholder")
                                                                                                  .Cast<TuPlaceholder>());
                return nElementList;
            }
        }

        public List<TuPlaceholder> PlaceholderFollowing
        {
            get
            {
                List<TuPlaceholder> nElementList = new List<TuPlaceholder>(ElementsFollowingTagName("tu-placeholder")
                                                                                                  .Cast<TuPlaceholder>());
                return nElementList;
            }
        }

        public List<Formatting> FormattingPreceding
        {
            get
            {
                List<Formatting> nElementList = new List<Formatting>(ElementsPrecedingTagName("formatting")
                                                                                                  .Cast<Formatting>());
                return nElementList;
            }
        }

        public List<Formatting> FormattingFollowing
        {
            get
            {
                List<Formatting> nElementList = new List<Formatting>(ElementsFollowingTagName("formatting")
                                                                                                  .Cast<Formatting>());
                return nElementList;
            }
        }

        public List<TuPlaceholder> PlaceholderPrecedingDirectly
        {
            get
            {
                List<TuPlaceholder> nElementList = PlaceholderPreceding.Intersect(ElementsPrecedingSameName.Last().ElementsFollowingTagName("tu-placeholder").Cast<TuPlaceholder>()).ToList();
                return nElementList;
            }
        }

        public string CharLimit
        {
            get
            {

                Regex regex = new Regex("max\\.(\\d+)\\.char");
                string sCharLimit = String.Empty;

                MatchCollection mCharLimit = regex.Matches(xNode.InnerText);
                if (mCharLimit.Count > 0) sCharLimit = mCharLimit[0].Groups[1].Value;

                return sCharLimit;
            }
        }

        /* Methods */

        /* Methods */

        /* Constructors */

        public Formatting()
        {

        }

        public Formatting(XmlNode xmlNode) : base(xmlNode)
        {
            if (!sName.Equals("formatting"))
            {
                throw new Exception(String.Format("XmlNode name is different than formatting."));
            }
        }
    }
}
