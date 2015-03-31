using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davide.Trotta.WebApi.Server.Model
{
    public class WhoIam
    {
        public string Name { get; set; }
        public IDictionary<string,string> Claims { get; set; }
    }
}
