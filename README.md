Web Api 2 Self Hosted and Bearer Token Authentication

A bear bone implementation for understanding the web.api 2 self host + OWIN + Bearer Token Authentication.

Important understand how generate claims (hardcoded in Token) and how to read the claims that the client will transmit to server
for each request.

[Authorize] attribute on controller will convert the token in claims.


    class Program
    {
        static void Main(string[] args)
        {
            var server = WebApp.Start<Startup>(url: "http://localhost:9000/");
            Console.WriteLine("Web API listening at http://localhost:9000/");

            Console.ReadLine();
        }
    }
    
    
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }                
            );

           

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
                
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
