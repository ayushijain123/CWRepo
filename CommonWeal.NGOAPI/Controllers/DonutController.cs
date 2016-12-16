using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;
using CommonWeal.Data.ModelExtension;
using System.Data.Entity.Core.Objects;

namespace CommonWeal.NGOAPI.Controllers
{
    public class DonutController : ApiController
    {
        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        //public HttpResponseMessage listdata()
        //{

        //    //List<NGOUser> list = new List<NGOUser>();
        //    DoNut obj = new DoNut();
        //    CommonWealEntities context = new CommonWealEntities();
        //    var obj2 = (ObjectResult<string>)context.usp_GetAllData() ;
        //    obj.Total = obj2.ToList();
        //    var response = Request.CreateResponse(HttpStatusCode.OK, obj.Total);
        //    return response;

        //}
        
         
        public HttpResponseMessage GetCount()
        {

            CommonWealEntities obj = new CommonWealEntities();
            var ngo = obj.NGOUsers;
            var users = obj.Users;
            Count cnt = new Count();
             //NGOs Table
            var COR =ngo.Where(w => w.IsActive == false && w.IsBlock == false).Count();
            var COA =ngo.Where(w => w.IsActive == true && w.IsBlock == false).Count();
             var COB = ngo.Where(w => w.IsBlock == true).Count();
            var COAU= users.Where(w => w.IsActive == true && w.IsBlock == false && w.LoginUserType == 3).Count();
           var COBU = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            var COWU = ngo.Where(w => w.IsWarn == true && w.IsBlock == false).Count();
            var COTAN = ngo.Where(w => w.IsActive == true).Count();
          var COTBN = ngo.Where(w => w.IsBlock == true).Count();
          var COTN = cnt.CountOfTotalActiveNGO + cnt.CountOfTotalBlockNGO;
        var COTAU = users.Where(w => w.IsActive == true && w.LoginUserType == 3).Count();
          var COTBU = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            var COTU = cnt.CountOfTotalActiveUsers + cnt.CountOfTotalBlockUsers;


            cnt.CountOfRequests = COR;
            cnt.CountOfActiveNGO = COA;
            cnt.CountOfBlockedNGO = COB;
            cnt.CountOfBlockedUsers =COBU;
            cnt.CountOfWarnedUsers =COWU;
            cnt.CountOfTotalActiveNGO = COTAN;
            cnt.CountOfTotalBlockNGO=COTBN;
            cnt.CountOfTotalNGO=COTN;
            cnt.CountOfTotalActiveUsers=COTAU;
            cnt.CountOfTotalBlockUsers=COTBU;
            cnt.CountOfTotalUsers = COTU;
            HttpResponseMessage res=Request.CreateResponse(HttpStatusCode.OK, cnt);
            return res;

        
        }
      

    }
}