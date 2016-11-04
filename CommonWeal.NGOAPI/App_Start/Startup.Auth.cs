using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using CommonWeal.NGOAPI.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(Startup))]
namespace CommonWeal.NGOAPI.App_Start
{
 
    public class Startup
    {
        


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
                        if (context.UserName == "test.test@mail.com" && context.Password == "test123")
                        {
                            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                            context.Validated(oAuthIdentity);
                        }
                    }
                },
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };

            app.UseOAuthBearerTokens(OAuthOptions);


            HttpConfiguration config = new HttpConfiguration();

           

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

		 
        }
    }
}
	 
