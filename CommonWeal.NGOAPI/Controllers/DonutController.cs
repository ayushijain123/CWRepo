using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;
using CommonWeal.Data.ModelExtension;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
namespace CommonWeal.NGOAPI.Controllers
{
    public class DonutController : ApiController
    {
        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
	/*added by ayushi*/
       
         public HttpResponseMessage GetCount()
            {
            /*added on 16-12-2016*/
            //CommonWealEntities obj = new CommonWealEntities();
            //var ngo = obj.NGOUsers;
            //var users = obj.Users;
            CommonWealEntities obj1 = new CommonWealEntities();
            var ngo = obj1.NGOUsers;
            var users = obj1.Users;
          
            var registeredUsers = obj1.RegisteredUsers;
            Count cnt = new Count();
             //NGOs Table
            var COR =ngo.Where(w => w.IsActive == false && w.IsBlock == false && w.IsDecline==false).Count();
            var COA =ngo.Where(w => w.IsActive == true && w.IsBlock == false).Count();
            var COB = ngo.Where(w => w.IsBlock == true).Count();
            //var COAU= users.Where(w => w.IsActive == true && w.IsBlock == false && w.LoginUserType == 3).Count();
            var COAU = obj1.RegisteredUsers.Include(x => x.User).Where(w => w.User.IsActive == true && w.User.LoginUserType == 3).Count(); 
            var COBU = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            var COWU = ngo.Where(w => w.IsWarn == true && w.IsBlock == false).Count();
            var COTAN = ngo.Where(w => w.IsActive == true).Count();
            var CountOfBlockedNGOS = users.Where(w => w.IsBlock == true&&w.LoginUserType==1).Count();
            var CountOfSpamNGOS = users.Where(w => w.IsSpam == true && w.LoginUserType == 1).Count();
            var CountOfDeclinedNGOS = users.Where(w => w.IsDecline == true && w.LoginUserType == 1).Count();
            var COTBN = CountOfBlockedNGOS + CountOfSpamNGOS + CountOfDeclinedNGOS;
            //var COTN = cnt.CountOfTotalActiveNGO + cnt.CountOfTotalBlockNGO;
            //var COTAU = users.Where(w => w.IsActive == true && w.LoginUserType == 3).Count();
            // var COTBU = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            //var COTU = cnt.CountOfTotalActiveUsers + cnt.CountOfTotalBlockUsers;
            var yearList = obj1.RegistrationYears.Select(x=>x.year).ToList();
            var CountOfBlockedUsers = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            var CountOfSpamUsers= users.Where(w => w.IsSpam == true && w.LoginUserType == 3).Count();
            var COTBU = CountOfBlockedUsers + CountOfSpamUsers;
            var requestestimation = obj1.DonationRequests.Where(x => x.IsRequest == true).Count();
         
          
            
           

            cnt.CountOfRequests = COR;
            cnt.CountOfActiveNGO = COA;
            cnt.CountOfTotalActiveUsers = COAU;
            cnt.CountOfBlockedNGO = COB;
            cnt.CountOfBlockedUsers =COBU;
            cnt.CountOfWarnedUsers =COWU;
            cnt.CountOfTotalActiveNGO = COTAN;
            cnt.CountOfTotalBlockNGO=COTBN;
            cnt.IsBlockSpam = COTBU;
            cnt.YearList = yearList;
            cnt.RequestEstimation = requestestimation;
            HttpResponseMessage res=Request.CreateResponse(HttpStatusCode.OK, cnt);
            return res;

        
        }
      

    }
}