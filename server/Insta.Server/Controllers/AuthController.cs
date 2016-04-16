using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Insta.Server.Infrastructure;
using InstaSharp;

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

        public async Task<ActionResult> OAuth(string code)
        {
            // add this code to the auth object
            var auth = new OAuth(config);

            // now we have to call back to instagram and include the code they gave us
            // along with our client secret
            var oauthResponse = await auth.RequestToken(code);

            // both the client secret and the token are considered sensitive data, so we won't be
            // sending them back to the browser. we'll only store them temporarily.  If a user's session times
            // out, they will have to click on the authenticate button again - sorry bout yer luck.
            HttpContext.Cache["InstaSharp.AuthInfo"] = oauthResponse;

            // all done, lets redirect to the home controller which will send some intial data to the app
            return RedirectToAction("Index", "Home");
        }
    }
}