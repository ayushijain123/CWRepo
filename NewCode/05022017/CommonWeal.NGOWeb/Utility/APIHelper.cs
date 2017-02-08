using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using CommonWeal.Data;
using Newtonsoft.Json;

namespace CommonWeal.NGOWeb.Utility
{
    public class ResponseData
    {
        public string Access_Token;
        public string Token_Type;
        public string Expires_In;
    }

    public class APIHelper<T> where T : class
    {
        public static string Access_Token
        {
            get
            {

                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session["Access_Token"] != null)
                {
                    return System.Web.HttpContext.Current.Session["Access_Token"].ToString();
                }

                return string.Empty;
            }
            set { System.Web.HttpContext.Current.Session["Access_Token"] = value; }
        }
        public static string Token_Type
        {
            get { return System.Web.HttpContext.Current.Session["Token_Type"] as string; }
            set { System.Web.HttpContext.Current.Session["Token_Type"] = value; }
        }
        public static string Expires_In
        {
            get { return System.Web.HttpContext.Current.Session["Expires_In"] as string; }
            set { System.Web.HttpContext.Current.Session["Expires_In"] = value; }
        }

        static string Token_Seperator = " ";

        const string applicationJson = "application/json";
        const string applicationOctet = "application/octet-stream";
        const string applicationformurlencoded = "application/x-www-form-urlencoded";
        static string APIServerUrl = System.Configuration.ConfigurationManager.AppSettings["APIServerUrl"];
        static string APIBaseUrl = APIServerUrl + System.Configuration.ConfigurationManager.AppSettings["APIBasePath"] + "/";
        static string APITokeUrl = APIServerUrl + System.Configuration.ConfigurationManager.AppSettings["APITokenPath"];
        /// <summary> 

        /// This propery will hold User token

        /// </summary>
        private static HttpClient GetHttpClient(string resourceURL)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(resourceURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJson));

            if (!string.IsNullOrWhiteSpace(Access_Token))
            {
                client.DefaultRequestHeaders.Add("Authorization", Token_Type + Token_Seperator + Access_Token);
            }
            return client;
        }

        public bool Login(string userName, string password, string grant_type)
        {
            string result = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIServerUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJson));
                // client.DefaultRequestHeaders.Add("Content-Type", contentType);
                // HTTP POST 
                // string payLoad = JsonConvert.SerializeObject(new { UserName = userName, Password = password, Grant_Type = grant_type });

                var payload = new List<KeyValuePair<string, string>> { 
                    new KeyValuePair<string, string>("username", userName),
                       new KeyValuePair<string, string>("password", password),
                          new KeyValuePair<string, string>("grant_Type", grant_type),
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(payload);
                content.Headers.Clear();
                content.Headers.Add("Content-Type", applicationformurlencoded);

                HttpResponseMessage response = client.PostAsync(APITokeUrl, content).ConfigureAwait(false).GetAwaiter().GetResult();
                result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("API error");
                }
                else
                {

                    var responseData = JsonConvert.DeserializeObject<ResponseData>(result);
                    Access_Token = responseData.Access_Token;
                    Token_Type = responseData.Token_Type;
                    Expires_In = responseData.Expires_In;
                }
            }
            return true;
        }

        public static void LogOut()
        {
            Access_Token = "";
            Token_Type = "";
            Expires_In = "";
        }

        public static T GetJson(string url)
        {
            url = APIBaseUrl + url;
            string result = "";
            using (HttpClient client = GetHttpClient(url))
            {
                // HTTP GET 
                HttpResponseMessage response = client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();
                result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("API error");
                }
            }
            return JsonConvert.DeserializeObject<T>(result); ;
        }

        public static async Task <T> GetJsonAsync1(string url)
        {
            url = APIBaseUrl + url;
            string result = "";
            using (HttpClient client = GetHttpClient(url))
            {
                // HTTP GET 
                HttpResponseMessage response =  client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();
                result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("API error");
                }
            }
            return JsonConvert.DeserializeObject<T>(result); ;
        }

        /// <summary>
        /// 
        //// create, update and delete 
        //// call when you want to handle serialization and de-serialization on your own 
        //// pass as normal string and accept normal string  
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        public static T PostJson<U>(string url, U payLoad)
        {
            string result = "";
            url = APIBaseUrl + url;
            using (HttpClient client = GetHttpClient(url))
            {
                string postData = JsonConvert.SerializeObject(payLoad);

                // HTTP POST 
                StringContent content = new StringContent(postData);
                //Added a check to identify it is a normal post request or delete request. 

                if (!url.Contains("Delete"))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(applicationJson);
                }
                HttpResponseMessage response = client.PostAsync(url, content).ConfigureAwait(false).GetAwaiter().GetResult();
                result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                   // throw new Exception("API error");
                    throw new Exception();
                }
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string UploadFile(string url, string filePath)
        {
            string result = "";
            url = APIBaseUrl + url;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamContent streamContent = new StreamContent(fileStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(applicationOctet);
                    using (HttpClient client = GetHttpClient(url))
                    {

                        HttpResponseMessage response = client.PostAsync(url, streamContent).Result;
                        result = response.Content.ReadAsAsync<String>().Result;
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception("API error");
                        }
                    }
                }
            }
            return result;
        }

        //public static async Task<T> GetJsonAsync(string url)
        //{

        //    string result = await GetJsonAsync(url);

        //    return  await JsonConvert.DeserializeObject<T>(result);

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<String> GetJsonAsync(string url)
        {
            String result = "";
            url = APIBaseUrl + url;
            using (HttpClient client = GetHttpClient(url))
            {

                //Get async, do not care about holding context, consumer of this method shoul hold context 
                HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception("API error"); ;
                }
            }
            return result;
        }

        public static async Task<String> PostJsonStringAsync<T>(string url, T payload)
        {
            return await PostJsonStringAsync(url, JsonConvert.SerializeObject(payload));
        }

        /// <summary>
        /// get async response for HTTP Post request 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static async Task<String> PostJsonStringAsync(string url, string payload)
        {
            String result = "";
            url = APIBaseUrl + url;
            using (HttpClient client = GetHttpClient(url))
            {
                // HTTP POST 
                StringContent content = new StringContent(payload);
                //Added a check to identify it is a normal post request or delete request. 
                if (!url.Contains("Delete"))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(applicationJson);
                }

                //Post async, do not care about holding context, consumer of this method shoul hold context 

                HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception("API error");
                }
            }
            return result;
        }


        public static string DownloadFile(string url, string payload, string filePath)
        {
            url = APIBaseUrl + url;
            string result = "";
            StringContent content = null;
            try
            {
                using (HttpClient client = GetHttpClient(url))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationOctet));
                    // HTTP POST 
                    content = new StringContent(payload);

                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    Stream stream = response.Content.ReadAsStreamAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("API error");
                    }
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fs);
                        fs.Close();
                    }
                }
            }
            finally
            {
                content.Dispose();
            }
            return result;
        }
    }
}
