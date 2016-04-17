using System.Collections.Generic;
using Insta.Crack.Model;
using Newtonsoft.Json;

namespace Insta.Crack.Services
{
	using System.Net.Http;

	public class InstaServerApi
	{
		private static string baseUrl = "http://localhost:1211";
		private static string feedUrl = $"{baseUrl}/api/feed";
		private static string loginUrl = $"{baseUrl}/auth/login";
		private static string tagUrl = $"{baseUrl}/api/tag?id=";

		public string LoginUser(string userName, string password)
		{
			var dictionry =new Dictionary<string, string>();
			using (var httpClient = new HttpClient())
			{
				var res =  httpClient
					.PostAsync(
						$"{loginUrl}?userName={userName}&password={password}",
						new FormUrlEncodedContent(dictionry))
					.Result;

				var key = res.Content.ReadAsStringAsync().Result;

				return JsonConvert.DeserializeObject<FuckThisInstaAuthShit>(key).Key;
			}
		}

		private class FuckThisInstaAuthShit
		{
			public string Key { get; set; }
		}

		public IList<InstaMedia> GetMyFeed(string instaKey)
		{
			using (var httpClient = new HttpClient())
			{
				var response = httpClient.GetStringAsync($"{feedUrl}?id={instaKey}").Result;
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