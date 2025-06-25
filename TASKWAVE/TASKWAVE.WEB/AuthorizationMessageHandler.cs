using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace TASKWAVE.WEB
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthorizationMessageHandler(IJSRuntime js)
        {
            _js = js;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Busca o token no localStorage
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                // Adiciona o header Authorization com Bearer token
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Continua a requisição normalmente
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
