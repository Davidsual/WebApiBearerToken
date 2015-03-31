using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Davide.Trotta.WebApi.Client.Model;

namespace Davide.Trotta.WebApi.Client
{
    public class ServiceRest
    {
        private string ClientId
        {
            get { return ConfigurationManager.AppSettings["ClientId"]; }
        }
        private string ClientSecret
        {
            get { return ConfigurationManager.AppSettings["ClientSecret"]; }
        }
        private string Username
        {
            get { return ConfigurationManager.AppSettings["Username"]; }
        }
        private string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }
        private string TokenUrl
        {
            get { return ConfigurationManager.AppSettings["TokenUrl"]; }
        }

        private string WhoIAmUrl
        {
            get { return ConfigurationManager.AppSettings["WhoIAmUrl"]; }
        }
        public async Task<Token> GetTokenAsync()
        {

            using (var client = new HttpClient())
            {
                var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", ClientId, ClientSecret)));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

                var form = new Dictionary<string, string>  
               {  
                   {"grant_type", "password"},  
                   {"username", Username},  
                   {"password",Password},  
               };

                var tokenResponse = await client.PostAsync(TokenUrl, new FormUrlEncodedContent(form));

                return await tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() });
            }

        }

        public async Task<string> WhoIAm(Token token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                var result = await client.GetAsync(WhoIAmUrl);

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
