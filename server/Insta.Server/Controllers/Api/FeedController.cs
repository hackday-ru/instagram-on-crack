using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Insta.Server.Infrastructure;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;

namespace Insta.Server.Controllers.Api
{
    public class FeedController : ApiController
    {
        // GET: api/Feed
        public async Task<IEnumerable<InstaMedia>> Get()
        {
            var imageToAsciiConverter = new ImageToAsciiConverter();
            var hhtpClient = new HttpClient();
            var feed = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/feed.json"));
            
            var instaResp  = JsonConvert.DeserializeObject<MediasResponse>(feed);
            var list = new List<InstaMedia>();
            foreach (var media in instaResp.Data)
            {
                var stream = await hhtpClient.GetStreamAsync(media.Images.LowResolution.Url);

                var instaMedia = new InstaMedia
                {
                    Media = media,
                    Data = imageToAsciiConverter.GetArrayImage(new Bitmap(stream), 100)
                };
                list.Add(instaMedia);
            }

            return list;
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
