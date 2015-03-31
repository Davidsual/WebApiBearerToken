using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Davide.Trotta.WebApi.Server
{
    public class APIErrorHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var jsonResult = "Error Global";

            context.Result = new ResponseMessageResult(new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, Content = new StringContent(jsonResult) });

            return Task.FromResult(0);
        }
    }
}
