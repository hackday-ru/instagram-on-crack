using System.Collections.Generic;
using Insta.Crack.Model;
using Newtonsoft.Json;

namespace Insta.Crack.Services
{
	using System.Net.Http;

	public class InstaServerApi
	{
		private static string feedUrl = "http://insta-on-crack.azurewebsites.net/api/feed";

		public IList<InstaMedia> GetMyFeed(string userName)
		{
			using (var httpClient = new HttpClient())
			{
				var response = httpClient.GetStringAsync(feedUrl).Result;
				return JsonConvert.DeserializeObject<IList<InstaMedia>>(response);
			}
		}
	}
}