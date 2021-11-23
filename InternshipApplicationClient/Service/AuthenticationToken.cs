using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Utils;

namespace InternshipApplicationClient.Service
{
    public class AuthenticationToken : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _browserStorage;

        public AuthenticationToken(HttpClient httpClient, ILocalStorageService browserStorage)
        {
            _httpClient = httpClient;
            _browserStorage = browserStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _browserStorage.GetItemAsync<string>(AuthenticationData.Token);
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ClaimReaderJWT.GetClaims(token), "jwt")));
        }
    }
}