using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.Utility
{
    public class APIHelper
    {
        static HttpClient client = new HttpClient();

        static string pathValue = System.Configuration.ConfigurationManager.AppSettings["API"];

        public static async Task<HttpResponseMessage> PostToAPI(string path, object postdata)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(pathValue + path, postdata);
            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource.
            return response;
        }
    }
}