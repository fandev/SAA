using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAA.Authorization.Security
{
    public class HttpsGuard : DelegatingHandler
    {
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (!request.RequestUri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.BadRequest, "HTTPS is required for security reason.");
                return Task.FromResult(reply);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}