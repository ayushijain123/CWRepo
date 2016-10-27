using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb
{
    public static class TypeHelper
    {

        public enum UserType
        {
            NGOAdmin = 1,
            Admin = 2,
            User = 3
        }

        public enum RegisteredWith
        {
            State_Government,
            Central_Government
        }
    }
}