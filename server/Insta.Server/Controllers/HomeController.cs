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
        private readonly InstagramConfig config;

        public HomeController()
        {
            var clientId = ConfigurationManager.AppSettings["client_id"];
            var clientSecret = ConfigurationManager.AppSettings["client_secret"];
            var redirectUri = ConfigurationManager.AppSettings["redirect_uri"];
            var realtimeUri = "";

            config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}