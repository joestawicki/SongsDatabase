using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using PianoSongs.Assets;

namespace PianoSongs.Assets
{
    public static class CollectionData
    {
        public static Dictionary<int, string> GetChoices()
        {
            Dictionary<int, string> choices = new Dictionary<int, string>();
            choices.Add(Constants.constEqualsKey, "Equals");
            choices.Add(Constants.constDoesNotEqualKey, "Does Not Equal");
            choices.Add(Constants.constContainsKey, "Contains");
            choices.Add(Constants.constDoesNotContainKey, "Does Not Contain");
            choices.Add(Constants.constBetweenKey, "Between");
            return choices;
        }
    }
}
