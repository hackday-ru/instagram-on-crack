using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Insta.Server.Infrastructure;
using Insta.Server.Models;
using InstaSharp;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;

namespace Insta.Server.Controllers.Api
{
    public class FeedController : BaseApiController
    {
		private static volatile Dictionary<string, IEnumerable<MediaModel>> _feedCache = new Dictionary<string, IEnumerable<MediaModel>>();

        // GET: api/Feed
        public async Task<IEnumerable<MediaModel>> Get(string id)
        {
	        if (id != null && _feedCache.ContainsKey(id))
	        {
		        return _feedCache[id];
	        }
            var instaResp = await GetFeed(id);

            var result = await FetchImagesAndConvertToASCII(instaResp);
	        _feedCache.Add(id, result);
	        return _feedCache[id];
        }

        private async Task<MediasResponse> GetFeed(string key)
        {
            var oAuthResponse = GetAuthResponse(key);

            if (oAuthResponse == null)
            {
                return null;
            }

            //var users = new InstaSharp.Endpoints.Media(_config, oAuthResponse);

            //var feed = await users.Popular();
            var httpClient = new HttpClient();
            var res =
                await
                    httpClient.GetStringAsync("https://api.instagram.com/v1/users/self/media/recent/?access_token=" + oAuthResponse.AccessToken);
            var feed = JsonConvert.DeserializeObject<MediasResponse>(res);
            return feed;
        }
    }
}
