using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFAutoCompleteTextbox
{
    public class AutoCompleteEntry
    {
        #region Variables
        private string[] keywordStrings;
        private string displayString;
        #endregion        

        #region Properties
        public AutoCompleteEntry(string name, params string[] keywords)
        {
            displayString = name;
            keywordStrings = keywords;
        }

        public string DisplayName
        {
            get { return displayString; }
            set { displayString = value; }
        }

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { displayString };
                }
                return keywordStrings;
            }
        }      
        #endregion

        #region Overridden Methods
        public override string ToString()
        {
            return displayString;
        }
        #endregion

        #region Methods
        #endregion        
    }
}
