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
    public class FeedController : ApiController
    {
        private readonly InstagramConfig _config;

        public FeedController()
        {
            var clientId = ConfigurationManager.AppSettings["client_id"];
            var clientSecret = ConfigurationManager.AppSettings["client_secret"];
            var redirectUri = ConfigurationManager.AppSettings["redirect_uri"];
            var realtimeUri = "";

            _config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
        }
        // GET: api/Feed
        public async Task<IEnumerable<MediaModel>> Get()
        {
            var imageToAsciiConverter = new ImageToAsciiConverter();
            var hhtpClient = new HttpClient();
            //var feed = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/feed.json"));
            
            //var instaResp  = JsonConvert.DeserializeObject<MediasResponse>(feed);
            var instaResp = await GetFeed();
            var list = new List<MediaModel>();
            foreach (var media in instaResp.Data)
            {
                var stream = await hhtpClient.GetStreamAsync(media.Images.StandardResolution.Url);

                var instaMedia = new MediaModel
                {
                    Media = media,
                    Data = imageToAsciiConverter.GetArrayImage(new Bitmap(stream), 100)
                };
                list.Add(instaMedia);
            }
            return list;
        }

        private async Task<MediasResponse> GetFeed()
        {
            var oAuthResponse = HttpContext.Current.Cache["InstaSharp.AuthInfo"] as OAuthResponse;

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

        // GET: api/Feed/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Feed
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Feed/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Feed/5
        public void Delete(int id)
        {
        }
    }
}
