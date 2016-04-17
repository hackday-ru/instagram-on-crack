using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Insta.Server.Infrastructure;
using InstaSharp;
using InstaSharp.Models.Responses;

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

        protected OAuthResponse GetAuthResponse(string key)
        {
            var store = (HttpContext.Cache["InstaSharp.AuthInfo"] as Dictionary<string, OAuthResponse>) ?? new Dictionary<string, OAuthResponse>();
            return store.ContainsKey(key) ? store[key] : null;
        }
    }
}