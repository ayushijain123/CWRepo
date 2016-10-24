using NGOUserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace NGOUserPage
{
    public class dbOperations
    {
        CommonWealEntities1 context = new CommonWealEntities1();
        public List<NGOUser> GetAllUserNotAccepted()
        {
            List<NGOUser> userList = new List<NGOUser>();
            userList = context.NGOUsers.Include(x => x.UserLogin).Where(w => w.UserLogin.IsActive== false).ToList();
            return userList;
        }
    }
}