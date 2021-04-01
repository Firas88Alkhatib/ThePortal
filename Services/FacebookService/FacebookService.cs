
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace ThePortal.Services.FacebookService
{
    public interface IFacebookService
    {
        Task<FacebookAccessTokenResponse> GetLonglivedAccessToken(string accesstoken);
        Task<FacebookAccount[]> GetAllAdAccounts(string accesstoken);
        Task<FacebookAd[]> GetAllAds(string adAccount, string accessToken);
    }
    public class FacebookService : IFacebookService
    {
        private readonly HttpClient _httpClient;
        private readonly FacebookApiKeys _apiKeys;
        public FacebookService(HttpClient httpClient, FacebookApiKeys apiKeys)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys;
        }
        private async Task<FacebookApiResponse<T>> FacebookRequest<T>(string accessToken, Uri url, HttpMethod method)
        {

            using HttpRequestMessage requestMessage = new(method, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadFromJsonAsync<FacebookErrorResponse>();
                throw new Exception(err.Error.Message);
            }
            return await response.Content.ReadFromJsonAsync<FacebookApiResponse<T>>();
        }
        public async Task<FacebookAccessTokenResponse> GetLonglivedAccessToken(string accessToken)
        {

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams.Add("grant_type", "fb_exchange_token");
            queryParams.Add("client_id", _apiKeys.ClientId);
            queryParams.Add("client_secret", _apiKeys.ClientSecret);
            queryParams.Add("fb_exchange_token", accessToken);
            Uri url = new("oauth/access_token?" + queryParams.ToString());
            var response = await FacebookRequest<FacebookAccessTokenResponse>("", url, HttpMethod.Get);
            return response.Data;
        }
        public async Task<FacebookAccount[]> GetAllAdAccounts(string accessToken)
        {
            Uri url = new("me/adaccounts?fields=name", UriKind.Relative);

            var facebookResponse = await FacebookRequest<FacebookAccount[]>(accessToken: accessToken, url: url, method: HttpMethod.Get);
            return facebookResponse.Data;
        }
        public async Task<FacebookAd[]> GetAllAds(string adAccount,string accessToken)
        {
            Uri url = new($"{adAccount}/ads?fields=name", UriKind.Relative);
            var response =  await FacebookRequest<FacebookAd[]>(accessToken,url,HttpMethod.Get);
            return response.Data;
        }
    }
}
