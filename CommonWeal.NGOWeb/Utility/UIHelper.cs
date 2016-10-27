using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Utility
{
    public class UIHelper
    {

        public static List<SelectListItem> GetDropDownListFromEnum(Type enumType)
        {

            //    if ( Type.GetType(enumType).IsAssignableFrom(enumType))
            //{

            //}

            IEnumerable<string> names = Enum.GetNames(enumType);
            IEnumerable<int> values = Enum.GetValues(enumType).Cast<int>();



            return names.Zip(values, (name, value) => new SelectListItem() { Value = value.ToString(), Text = name.Replace("_"," " )}).ToList();


        }
    }
}