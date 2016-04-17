using InstaSharp;
using InstaSharp.Endpoints;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Insta.Server.Infrastructure
{
    public class Instagram : IDisposable
    {
        private const string USER_AGENT =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/45.0.2414.0 Safari/537.36";

        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;

        public static async Task<OAuthResponse> AuthByCredentialsAsync(string username, string password,
            InstagramConfig config, List<OAuth.Scope> scopes)
        {
            using (var instagram = new Instagram())
            {
                if (await instagram.LoginAsync(username, password))
                {
                    return await instagram.GetOauthResponse(config, scopes);
                }
            }

            throw new Exception("Authentification error");
        }

        public async Task<OAuthResponse> GetOauthResponse(InstagramConfig config, List<OAuth.Scope> scopes)
        {
            var token = await GetAccessToken(config.ClientId, config.RedirectUri, BuildScopeForUri(scopes));

            var auth = new OAuthResponse();
            auth.AccessToken = token;
            auth.User = new User();

            var users = new Users(config, auth);
            var self = await users.GetSelf();
            auth.User = self.Data;

            return auth;
        }

        private static string BuildScopeForUri(List<OAuth.Scope> scopes)
        {
            var scope = new StringBuilder();

            foreach (var s in scopes)
            {
                if (scope.Length > 0)
                {
                    scope.Append("+");
                }
                scope.Append(s.ToString().ToLower());
            }

            return scope.ToString();
        }


        public async Task<string> GetAccessToken(string clientId, string redirectUri, string scope)
        {
            var requestUri = string.Format(
                "/oauth/authorize/?client_id={0}&redirect_uri={1}&scope=basic+likes+comments&response_type=token",
                WebUtility.UrlEncode(clientId),
                WebUtility.UrlEncode(redirectUri),
                WebUtility.UrlEncode(scope)
                );

            var fields = new Dictionary<string, string>()
            {
                {"csrfmiddlewaretoken", GetCSRFToken()},
                {"allow", "Authorize"}
            };

            var uri = new Uri("https://api.instagram.com");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Referrer = new Uri(_httpClient.BaseAddress, requestUri);
            request.Content = new FormUrlEncodedContent(fields);

            string tokenFragment;

            using (var temporaryHandler = new HttpClientHandler())
            {
                temporaryHandler.CookieContainer = _httpClientHandler.CookieContainer;

                using (var temporaryClient = new HttpClient(temporaryHandler))
                {
                    temporaryClient.BaseAddress = uri;

                    using (var response = await temporaryClient.SendAsync(request))
                    {
                        tokenFragment = request.RequestUri.Fragment;
                    }
                }
            }

            var position = tokenFragment.IndexOf('=');
            return tokenFragment.Substring(position + 1);
        }

        public HttpClient Client
        {
            get { return _httpClient; }
        }

        public Instagram()
        {
            _httpClientHandler = new HttpClientHandler();

            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.BaseAddress = new Uri("https://www.instagram.com/");
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _httpClientHandler.Dispose();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var res = await _httpClient.GetAsync("/accounts/login/?force_classic_login");

            string csrftoken = GetCSRFToken();

            var fields = new Dictionary<string, string>()
            {
                {"username", username},
                {"password", password}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/accounts/login/ajax/");
            request.Content = new FormUrlEncodedContent(fields);

            request.Headers.Referrer = new Uri(_httpClient.BaseAddress, "/accounts/login/");
            request.Headers.Add("X-CSRFToken", csrftoken);
            request.Headers.Add("X-Instagram-AJAX", "1");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            using (var response = await _httpClient.SendAsync(request))
            {
                var info = JsonConvert.DeserializeObject<LoginInfo>(await response.Content.ReadAsStringAsync());
                return info.authenticated;
            }
        }
        private string GetCSRFToken()
        {
            var cookies = _httpClientHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);
            return cookies["csrftoken"].Value;
        }

        private class LoginInfo
        {
            public string status { get; set; }
            public bool authenticated { get; set; }
        }

    }
}