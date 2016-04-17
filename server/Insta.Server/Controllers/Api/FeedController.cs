using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Insta.Server.Models;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;

namespace Insta.Server.Controllers.Api
{
    public class FeedController : BaseApiController
    {
        // GET: api/Feed
        public async Task<IEnumerable<MediaModel>> Get(string id)
        {
            var instaResp = await GetFeed(id);

            return await FetchImagesAndConvertToASCII(instaResp);
        }

        private async Task<MediasResponse> GetFeed(string key)
        {
            var oAuthResponse = GetAuthResponse(key);

            if (oAuthResponse == null)
            {
                return null;
            }
            
            var httpClient = new HttpClient();
            var res =
                await
                    httpClient.GetStringAsync("https://api.instagram.com/v1/users/self/media/recent/?access_token=" + oAuthResponse.AccessToken);
            var feed = JsonConvert.DeserializeObject<MediasResponse>(res);
            return feed;
        }
    }
}
