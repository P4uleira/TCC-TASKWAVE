using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.WEB.Authentication
{
    public class ApiService(HttpClient httpClient, ProtectedLocalStorage localStorage)
    {

        public async Task SetAuthorizeHeaderAsync()
        {
            var token = (await localStorage.GetAsync<string>("authToken")).Value;
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

    }
}
