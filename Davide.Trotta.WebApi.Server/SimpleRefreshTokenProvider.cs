using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Davide.Trotta.WebApi.Server
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();
            
            // maybe only create a handle the first time, then re-use
            _refreshTokens.TryAdd(guid, context.Ticket);
            context.Ticket.Properties.AllowRefresh = true;
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(2)); 
            // consider storing only the hash of the handle
            context.SetToken(guid);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type" });

            AuthenticationTicket ticket;
            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
            //return Task.FromResult<object>(null);
        }
    }
}
