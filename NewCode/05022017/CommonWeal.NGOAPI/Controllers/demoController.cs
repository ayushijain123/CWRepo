using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;


namespace CommonWeal.NGOAPI.Controllers
{
    public class demoController : ApiController
    {


        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Get1(int year = 0)
        {

            //var year = 2016;

            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            CommonWealEntities context = new CommonWealEntities();
            List<int> ngo = new List<int>();
            List<int> user = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                ngo.Add(context.Users.Where(x => x.LoginUserType == 1 && x.CreatedOn.Value.Month == i && x.CreatedOn.Value.Year == year).Count());
                user.Add(context.Users.Where(x => x.LoginUserType == 3 && x.CreatedOn.Value.Month == i && x.CreatedOn.Value.Year == year).Count());

            }
            List<BarChartModel> barChartData = new List<BarChartModel>();
            barChartData.Add(new BarChartModel()
            {
                name = "NGO",
                data = ngo
            });

            barChartData.Add(new BarChartModel()
            {
                name = "User",
                data = user
            });

            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, barChartData);
            return res;

        }
        public class BarChartModel
        {
            public string name { get; set; }
            public List<int> data { get; set; }
        }


    }
}
