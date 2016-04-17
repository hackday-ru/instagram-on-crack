using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Insta.Server.Infrastructure;
using InstaSharp;
using InstaSharp.Models.Responses;

namespace Insta.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly InstagramConfig config;

        public AuthController()
        {
            var clientId = ConfigurationManager.AppSettings["client_id"];
            var clientSecret = ConfigurationManager.AppSettings["client_secret"];
            var redirectUri = ConfigurationManager.AppSettings["redirect_uri"];
            var realtimeUri = "";

            config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
        }
        

        public ActionResult Login()
        {
            var scopes = new List<OAuth.Scope>
            {
                InstaSharp.OAuth.Scope.Basic,
                InstaSharp.OAuth.Scope.Likes,
                InstaSharp.OAuth.Scope.Comments,
                InstaSharp.OAuth.Scope.Follower_List,
                InstaSharp.OAuth.Scope.Public_Content,
                InstaSharp.OAuth.Scope.Relationships
            };

            var link = InstaSharp.OAuth.AuthLink(config.OAuthUri + "authorize", config.ClientId, config.RedirectUri,
                scopes, InstaSharp.OAuth.ResponseType.Code);

            return Redirect(link);
        }

        [HttpPost]
        public async Task<JsonResult> Login(string username, string password)
        {
            var scopes = new List<OAuth.Scope> { InstaSharp.OAuth.Scope.Basic };
            var oAuthResponse = await Instagram.AuthByCredentialsAsync(username, password, config, scopes);

            var guid  = AddAuthResponseToStore(oAuthResponse);

            return Json(new { Key = guid });
        }


        public async Task<ActionResult> OAuth(string code)
        {
            var auth = new OAuth(config);

            var oauthResponse = await auth.RequestToken(code);

            var guid = AddAuthResponseToStore(oauthResponse);

            Response.SetCookie(new System.Web.HttpCookie("identity", guid));

            return RedirectToAction("Index", "Home");
        }

        private string AddAuthResponseToStore(OAuthResponse oauthResponse)
        {
            var store = (HttpContext.Cache["InstaSharp.AuthInfo"] as Dictionary<string, OAuthResponse>) ?? new Dictionary<string, OAuthResponse>();
            var guid = System.Guid.NewGuid().ToString();
            store.Add(guid, oauthResponse);
            HttpContext.Cache["InstaSharp.AuthInfo"] = store;
            return guid;
        }
    }
}