using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOAPI.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(Startup))]
namespace CommonWeal.NGOAPI.App_Start
{

    public class Startup
    {

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),

                Provider = new OAuthAuthorizationServerProvider()
                {
                    OnValidateClientAuthentication = async (context) =>
                    {
                        context.Validated();
                    },
                    OnGrantResourceOwnerCredentials = async (context) =>
                    {
                        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                        if (!string.IsNullOrWhiteSpace(context.UserName) && !string.IsNullOrWhiteSpace(context.Password))
                        {
                            CommonWeal.Data.CommonWealEntities db = new Data.CommonWealEntities();

                            var loginuser = db.Users.Where(usr => usr.LoginEmailID.ToLower() == context.UserName.ToLower() && usr.LoginPassword == context.Password).FirstOrDefault();

                            if (loginuser != null)
                            {

                                // Create or retrieve a ClaimsIdentity to represent the 
                                // Authenticated user:
                                ClaimsIdentity identity =
                                    new ClaimsIdentity(context.Options.AuthenticationType);
                                identity.AddClaim(new Claim(ClaimTypes.Email, loginuser.LoginEmailID));

                                identity.AddClaim(new Claim(ClaimTypes.Name, loginuser.LoginEmailID));
                                identity.AddClaim(new Claim(ClaimTypes.Sid, loginuser.LoginID.ToString()));
                                // Add a Role Claim:
                                identity.AddClaim(new Claim(ClaimTypes.Role, ((TypeHelper.UserType)loginuser.LoginUserType).ToString()));

                                // Identity info will ultimatly be encoded into an Access Token
                                // as a result of this call:
                                context.Validated(identity);

                            }
                        }

                    },


                },

                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)

            };


            app.UseOAuthBearerTokens(OAuthOptions);


            OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
            {

            };

            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            HttpConfiguration config = new HttpConfiguration();

            FilterConfig.RegisterGlobalFilters(config);



            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
           
            

        }
    }
}

