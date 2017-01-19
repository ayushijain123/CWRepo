using CommonWeal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class DonationData
{
    public string Image { get; set; }
    public string Description { get; set; }
    public int RequestNGOID { get; set; }
    //public string ItemName { get; set; }
    //public int  ItemCount { get; set; }
    public List<DonationDetail> donationdetaildata{get; set;}
}

namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class DonationController : BaseController
    {

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "ASJ", "AJ" };
        //}

        [HttpPost]
        public HttpResponseMessage NGODonationRequest(DonationData donationdata)
        {
            CommonWealEntities context = new CommonWealEntities();
            DonationRequest donationrequest = new DonationRequest();
            donationrequest.ImgeUrl = donationdata.Image;
            donationrequest.Description = donationdata.Description;
            donationrequest.RequestNGOId = donationdata.RequestNGOID;
            donationrequest.createdOn = DateTime.Now;
            context.DonationRequests.Add(donationrequest);
           // context.SaveChanges();
            var ItemTypes = donationdata.donationdetaildata.Count();
            DonationDetail donationdetail = new DonationDetail();
            for (int i = 0; i < ItemTypes; i++)
            {
                donationdetail = donationdata.donationdetaildata[i];
                donationdetail.RequestID = donationrequest.RequestID;
                donationdetail.DonatedCount=0;
                donationdetail.ItemRequire = donationdetail.ItemCount ;
                //donationdetail.ItemName = donationdata.donationdetaildata.it
                //donationdetail.ItemCount = donationdata.ItemCount;
                donationdetail.createdOn = DateTime.Now;
                context.DonationDetails.Add(donationdetail);
                
            }
            context.SaveChanges();
           // var res = "hello";
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        public class UserDonationData
        {
            public int donatecount { get; set; }
            public int ItemID { get; set; }
            public int UserLoginID { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage UserDonation(UserDonationData userdonationdata)
        {
            CommonWealEntities context = new CommonWealEntities();
            DonarDetail donardetail = new DonarDetail();
            donardetail.ItemID = userdonationdata.ItemID;
            donardetail.DonarLoginID = userdonationdata.UserLoginID;
            donardetail.createdOn = DateTime.Now;
          
            var donationdeatail = context.DonationDetails.Where(w=>w.ItemID == donardetail.ItemID).FirstOrDefault();
            donationdeatail.DonatedCount = donationdeatail.DonatedCount+ userdonationdata.donatecount;
            donationdeatail.ItemRequire = donationdeatail.ItemCount - donationdeatail.DonatedCount;
            context.DonarDetails.Add(donardetail);
            context.SaveChanges();
           // var res = "hello";
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public class GetDonationData 
        {
            public List<DonationRequest> donationrequest { get; set; }
            public List<DonationDetail> dontiondetail { get; set; }
            public List<DonarDetail> donardetail { get; set; }
        }
        [HttpGet]
        public HttpResponseMessage GetDonationDetail()
        {
            CommonWealEntities context = new CommonWealEntities();
            var requestvalue = context.DonationRequests.ToList();
            GetDonationData getdonationdata = new GetDonationData();
            getdonationdata.donationrequest = requestvalue;
            var requestdetail = context.DonationDetails.ToList();
            getdonationdata.dontiondetail = requestdetail;
            var donar = context.DonarDetails.ToList();
            getdonationdata.donardetail = donar;
           
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, getdonationdata);
            return response;
        }
    }
}
