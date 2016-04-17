using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
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
	public class TagController : BaseApiController
	{
		private static volatile Dictionary<string, IEnumerable<MediaModel>> _tageCache = new Dictionary<string, IEnumerable<MediaModel>>();

		public async Task<IEnumerable<MediaModel>> Get(string id)
		{
			if (id != null && _tageCache.ContainsKey(id))
			{
				return _tageCache[id];
			}

			var instaResp = await GetFeed(id);

            var result = await FetchImagesAndConvertToASCII(instaResp);
			_tageCache.Add(id, result);
			return _tageCache[id];
		}

        private async Task<MediasResponse> GetFeed(string id = "hackday")
		{
			var accessToken = "16384709.6ac06b4.49b97800d7fd4ac799a2c889f50f2587";
			var httpClient = new HttpClient();
			var res =
				await
					httpClient.GetStringAsync("https://api.instagram.com/v1/tags/" + id + "/media/recent?access_token=" + accessToken);
			var feed = JsonConvert.DeserializeObject<MediasResponse>(res);
			return feed;
		}

	}
}
