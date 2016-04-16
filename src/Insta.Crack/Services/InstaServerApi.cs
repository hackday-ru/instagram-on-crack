using System.Collections.Generic;
using Insta.Crack.Model;
using Newtonsoft.Json;

namespace Insta.Crack.Services
{
	using System.Net.Http;

	public class InstaServerApi
	{
		private static string feedUrl = "http://insta-on-crack.azurewebsites.net/api/feed";
		private static string tagUrl = "http://insta-on-crack.azurewebsites.net/api/tag?id=";

		public IList<InstaMedia> GetMyFeed(string userName)
		{
			using (var httpClient = new HttpClient())
			{
				var response = httpClient.GetStringAsync(feedUrl).Result;
				return JsonConvert.DeserializeObject<IList<InstaMedia>>(response);
			}
		}

		public IList<InstaMedia> GetTagFeed(string tagName)
		{
			using (var httpClient = new HttpClient())
			{
				var response = httpClient.GetStringAsync(tagUrl + tagName).Result;
				return JsonConvert.DeserializeObject<IList<InstaMedia>>(response);
			}
		}
	}
}