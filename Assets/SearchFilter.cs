using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Reflection;

namespace PianoSongs.Assets
{
    public static class SearchFilter
    {
        public static void populateDropdown(Combobox a)
        {
        }

        public static Dictionary<int, string> GetListItems(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ApplicationException("GetListItems does not support non-enum types");
            Dictionary<int, string> list = new Dictionary<int, string>();
            foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                int value;
                string display;
                value = (int)field.GetValue(null);
                display = Enum.GetName(enumType, value);
                foreach (Attribute currAttr in field.GetCustomAttributes(true))
                {
                    EnumValueDataAttribute valueAttribute = currAttr as EnumValueDataAttribute;
                    if (valueAttribute != null)
                        display = valueAttribute.Name;
                }
                list.Add(value, display);
            }
            return list;
        }

        public class EnumValueDataAttribute : Attribute
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }

        public enum SearchFilter
        {
            Equals,
            [EnumValueData(Name="Does Not Equal")]
            DoesNotEqual,
            Contains,
            [EnumValueData(Name = "Does Not Contain")]
            DoesNotContain
        };

    }
}
