using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Utility
{
    public static class UIHelper
    {

        public static IEnumerable<SelectListItem> GetDropDownListFromEnum(this Type enumType)
        {

            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
            }
            IEnumerable<string> names = Enum.GetNames(enumType);
            IEnumerable<int> values = Enum.GetValues(enumType).Cast<int>();


            var items = names.Zip(values, (name, value) =>
                 new SelectListItem()
                 {
                     Value = value.ToString(),
                     Text = name.Replace("_", " ")
                 });

            return items;

        }
    }
}