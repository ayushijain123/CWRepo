using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGOUserPage
{
    public class dbOperation
    {
        commonwealdbEntities context = new commonwealdbEntities();

        //insert data for ngo
        public bool Insert(tbl_ngo obj)
        {
            try
            {
                context.tbl_ngo.Add(obj);
                context.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        //insert data for user
        public bool Insert(tbl_user obj)
        {
            try
            {
                context.tbl_user.Add(obj);
                context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        //insert comman data for user and ngo
        public long Insert(tbl_login obj)
        {
            try
            {
                context.tbl_login.Add(obj);
                context.SaveChanges();
                var ob = context.tbl_login.Where(w => w.login_Email == obj.login_Email).FirstOrDefault();
                var id = ob.login_id;
                return id;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        public int userLogin(string userEmail, string password)
        {
            var ob = context.tbl_login.Where(w => w.login_Email == userEmail).FirstOrDefault();
            if (ob.login_Email == userEmail && ob.login_password == password)
            {
                if (ob.isAccepted == 0)
                {

                    return 3;//if user is not varified by admin

                }
                return 1;//if user is verifid by admin
            }
            else
            {
                return 0;// if user entered invalid credentials
            }

        }
        //verify that email exist in the database or not
        public bool CheckEmail(String email)
        {

            var ob = context.tbl_login.Where(w => w.login_Email == email).FirstOrDefault();
            if (ob != null)
            {
                return true;
            }

            else
            {
                return false;

            }
        }

        //update password in the database
        public bool updatePwd(String email, String newpwd)
        {
            
            
            var login_ob = context.tbl_login.Where(w => w.login_Email == email).FirstOrDefault();
            if (login_ob != null) {

                login_ob.login_password = newpwd;
                if (login_ob.login_userType == "NGO")
                { var ngo = context.tbl_ngo.Where(w => w.ngo_Email == email).FirstOrDefault();
                ngo.ngo_password = newpwd;
                }
                else if (login_ob.login_userType == "USER")
                {
                    var user = context.tbl_user.Where(w => w.login_Email == email).FirstOrDefault();
                    user.login_password= newpwd;
                }
                context.SaveChanges();
                return true;
            
            
            }
          
            else
            {
                return false;
            }


        }
        public List<operationalArea> GetArea()
        {
            var ob=context.operationalArea.ToList();
            return ob;
        
        }
        public List<AreaOfInterest> getList()
        {
            var ao = context.AreaOfInterest.ToList();
                    return ao;
        }


    }

}




