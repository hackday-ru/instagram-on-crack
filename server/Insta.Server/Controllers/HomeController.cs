using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Insta.Server.Infrastructure;
using InstaSharp;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;

namespace Insta.Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var key = Request.Cookies["identity"]?.Value;
            var auth = GetAuthResponse(key);
            ViewBag.Key = key;
            return View(auth?.User);
        }

        public async Task<ActionResult> Feed(string id)
        {
            var key = Request.Cookies["identity"]?.Value;
            var auth = GetAuthResponse(key);
            if (auth == null && string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            var httpClient = new HttpClient();
            var tagAccessToken = "16384709.6ac06b4.49b97800d7fd4ac799a2c889f50f2587";
            var url =
                string.IsNullOrWhiteSpace(id)
                    ? "https://api.instagram.com/v1/users/self/media/recent/?access_token=" + auth.AccessToken
                    : "https://api.instagram.com/v1/tags/" + id + "/media/recent?access_token=" + tagAccessToken;

            ViewBag.Tag =
                string.IsNullOrWhiteSpace(id)
                    ? "@" + auth.User.Username
                    : "#" + id;
            var res =
                await
                    httpClient.GetStringAsync(url);
            var feed = JsonConvert.DeserializeObject<MediasResponse>(res);
                
            return View(feed.Data);
        }

        protected OAuthResponse GetAuthResponse(string key)
        {
            var store = (HttpContext.Cache["InstaSharp.AuthInfo"] as Dictionary<string, OAuthResponse>) ?? new Dictionary<string, OAuthResponse>();
            return !string.IsNullOrWhiteSpace(key) && store.ContainsKey(key) ? store[key] : null;
        }
    }
}