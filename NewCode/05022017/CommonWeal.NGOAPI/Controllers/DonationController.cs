using CommonWeal.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CommonWeal.Data.ModelExtension;



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
        public bool NGODonationRequest(DonationData donationdata)
        {
            CommonWealEntities context = new CommonWealEntities();
            DonationRequest donationrequest = new DonationRequest();           
           // donationrequest.ImgeUrl = donationdata.Image;
            donationrequest.Description = donationdata.Description;
            donationrequest.RequestNGOId = donationdata.RequestNGOID;
            donationrequest.createdOn = DateTime.Now;
            donationrequest.Status = false;
            if (donationdata.Image != null)
            {
                byte[] imageBytes = Convert.FromBase64String(donationdata.Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                var abc = Guid.NewGuid();
                string path = "/Images/Post/" + abc + ".jpg";
                string filepath = HttpContext.Current.Server.MapPath(path);
                image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                donationrequest.ImgeUrl = path;              
            }
            donationrequest.ItemCost = 0;
            context.DonationRequests.Add(donationrequest);
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
            var ItemTypes = donationdata.donationdetaildata.Count();
            DonationDetail donationdetail = new DonationDetail();
            for (int i = 0; i < ItemTypes; i++)
            {
                donationdetail = donationdata.donationdetaildata[i];
                donationdetail.RequestID = donationrequest.RequestID;
                donationdetail.DonatedCount = 0;
                donationdetail.ItemRequire = donationdetail.ItemCount;
                //donationdetail.ItemName = donationdata.donationdetaildata.it
                //donationdetail.ItemCount = donationdata.ItemCount;
                donationdetail.createdOn = DateTime.Now;
                donationdetail.DonatedCost = 0;

                context.DonationDetails.Add(donationdetail);

            }
            NGOPost ngopost = new NGOPost();
            ngopost.CreatedOn = DateTime.Now;
            ngopost.PostDateTime = DateTime.Now;
            ngopost.Isdelete = false;
            ngopost.IsRequest = true;
            ngopost.PostContent = donationdata.Description;
            ngopost.LoginID = donationdata.RequestNGOID;
            ngopost.PostCommentCount = 0;
            ngopost.PostLikeCount = 0;
            ngopost.PostUrl = donationrequest.ImgeUrl;
            ngopost.RequestID = donationrequest.RequestID;
            context.NGOPosts.Add(ngopost);
            context.SaveChanges();
           // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, res);
            return true;
        }



        public class UserDonationData
        {
            public int donatecount { get; set; }
            public int ItemID { get; set; }
            public int UserLoginID { get; set; }
            public int RequestId { get; set; }
        }
        public class DonarDetailData
        {
            public List<UserDonationData> donardetailvalue { get; set; }
        }

        [HttpPost]
        public bool UserDonation(DonarDetailData donardetaildata)
        {
            CommonWealEntities context = new CommonWealEntities();
            DonarDetail donardetail = new DonarDetail();
            var count = donardetaildata.donardetailvalue.Count;
            var result = false;
            for (int i = 0; i < count; i++)
            {
                var itemid = donardetaildata.donardetailvalue[i].ItemID;
                var donationdetail = context.DonationDetails.Where(w => w.ItemID == itemid).FirstOrDefault();

                var userid = donardetaildata.donardetailvalue[i].UserLoginID;
                var donar = context.DonarDetails.Where(w => w.ItemID ==itemid && w.DonarLoginID==userid).FirstOrDefault();
                if (donar !=null)
                {
                   //donar.ItemID = donardetaildata.donardetailvalue[i].ItemID;
                    //donar.DonarLoginID = donardetaildata.donardetailvalue[i].UserLoginID;
                   // donardetail.createdOn = DateTime.Now;

                    // donardetail.RequestId = donardetaildata.donardetailvalue[i].RequestId;

                    donar.Donatecount =donar.Donatecount+ donardetaildata.donardetailvalue[i].donatecount;

                    //donardetail.RequestId = donationdetail.RequestID;
                    donationdetail.DonatedCount = donationdetail.DonatedCount + donardetaildata.donardetailvalue[i].donatecount;
                    var remaining = donationdetail.ItemCount - donationdetail.DonatedCount;
                    donationdetail.ItemRequire = remaining > 0 ? remaining : 0;
                    //context.DonarDetails.Add(donardetail);
                }
                else
                {
                    donardetail.ItemID = donardetaildata.donardetailvalue[i].ItemID;
                    donardetail.DonarLoginID = donardetaildata.donardetailvalue[i].UserLoginID;
                    donardetail.createdOn = DateTime.Now;

                    // donardetail.RequestId = donardetaildata.donardetailvalue[i].RequestId;

                    donardetail.Donatecount = donardetaildata.donardetailvalue[i].donatecount;

                    donardetail.RequestId = donationdetail.RequestID;
                    donationdetail.DonatedCount = donationdetail.DonatedCount + donardetaildata.donardetailvalue[i].donatecount;
                    var remaining = donationdetail.ItemCount - donationdetail.DonatedCount;
                    donationdetail.ItemRequire = remaining > 0 ? remaining : 0;
                    context.DonarDetails.Add(donardetail);
                 
                }
                   context.SaveChanges();
                result = true;
            }


            // var res = "hello";
            // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return result;
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
