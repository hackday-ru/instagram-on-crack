using Insta.Server.Infrastructure;
using Insta.Server.Models;
using InstaSharp;
using InstaSharp.Models.Responses;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Insta.Server.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected readonly InstagramConfig _config;

        public BaseApiController()
        {
            var clientId = ConfigurationManager.AppSettings["client_id"];
            var clientSecret = ConfigurationManager.AppSettings["client_secret"];
            var redirectUri = ConfigurationManager.AppSettings["redirect_uri"];
            var realtimeUri = "";

            _config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
        }

        protected OAuthResponse GetAuthResponse(string key)
        {
            var store = (HttpContext.Current.Cache["InstaSharp.AuthInfo"] as Dictionary<string, OAuthResponse>) ?? new Dictionary<string, OAuthResponse>();
            return !string.IsNullOrWhiteSpace(key) && store.ContainsKey(key) ? store[key] : null;
        }

        protected async Task<IEnumerable<MediaModel>> FetchImagesAndConvertToASCII(MediasResponse instaResp)
        {

            var imageToAsciiConverter = new ImageToAsciiConverter();
            var hhtpClient = new HttpClient();

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
    }
}