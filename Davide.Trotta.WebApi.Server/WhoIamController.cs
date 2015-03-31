using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Davide.Trotta.WebApi.Server.Model;

namespace Davide.Trotta.WebApi.Server
{
    [Authorize]
    public class WhoIamController : ApiController
    {
        [Route("api/WhoIAm")]
        public WhoIam Get()
        {
            var userName = User.Identity.Name;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var allClaims = claimsIdentity.Claims.ToList();
            
            return new WhoIam
            {
                Name = User.Identity.Name,
                Claims = allClaims.Select(item => new KeyValuePair<string,string>(item.Type,item.Value)).ToDictionary(i => i.Key,a=>a.Value)
            };
        }
    }
}
