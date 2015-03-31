using System;
using System.Collections;
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
    public class DetailsController : ApiController
    {
        [Route("api/Details")]
        public IEnumerable<Detail> Get()
        {

            return new List<Detail>()
            {              
                new Detail()
                {
                    Firstname = "Davide",
                    Lastname = "Trotta",
                    DateOfBirth = DateTime.Now,
                    Email = "trotta@hotmail.com"
                }
            };
        }
    }
}
