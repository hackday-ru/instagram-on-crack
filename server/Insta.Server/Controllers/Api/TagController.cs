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
	public class TagController : ApiController
	{
		private readonly InstagramConfig _config;
		public TagController()
		{
			var clientId = ConfigurationManager.AppSettings["client_id"];
			var clientSecret = ConfigurationManager.AppSettings["client_secret"];
			var redirectUri = ConfigurationManager.AppSettings["redirect_uri"];
			var realtimeUri = "";

			_config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
		}

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
