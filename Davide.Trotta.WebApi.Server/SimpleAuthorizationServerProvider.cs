﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Davide.Trotta.WebApi.Server
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials 
        /// for that client are present on the request. If the web application accepts Basic authentication credentials, 
        /// context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if 
        /// present in the request header. If the web application accepts "client_id" and "client_secret" as form encoded 
        /// POST parameters, context.TryGetFormCredentials(out clientId, out clientSecret) may be called to 
        /// acquire those values if present in the request body. If context.Validated is not called the request will not proceed further.
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId;
            string clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                if (clientId == "CustomClientId" && clientSecret == "3BE3B807-0F78-442E-8368-6DB726A1BAAC")
                {
                    context.Validated();
                }
            }  


          //  context.Validated();



        }
        

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". 
        /// This occurs when the user has provided name and password credentials directly into the 
        /// client application's user interface, and the client application is using those to acquire an 
        /// "access_token" and optional "refresh_token". If the web application supports the resource owner 
        /// credentials grant type it must validate the context.Username and context.Password as appropriate. 
        /// To issue an access token the context.Validated must be called with a new ticket containing the 
        /// claims about the resource owner which should be associated with the access token. 
        /// The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers. 
        /// The default behavior is to reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type" });


            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));
            //identity.AddClaim(new Claim(ClaimTypes.Name, "sachaAndDavide"));
            //context.Validated(identity);


            if (context.UserName == "Pussy" && context.Password == "Cat")
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", "test"));
                identity.AddClaim(new Claim("role", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                context.Validated(identity);
                return;
            }
            context.Rejected(); 

        }
    }
}
