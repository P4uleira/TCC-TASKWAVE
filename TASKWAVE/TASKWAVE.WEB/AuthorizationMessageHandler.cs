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


    }
}
