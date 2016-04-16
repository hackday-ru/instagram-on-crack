using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insta.Server.Infrastructure;
using System.Web;
using System.Web.Mvc;
using Insta.Server.Models;
using InstaSharp.Models;

namespace Insta.Server.Controllers
{
    public class ImageController : Controller
    {
        // GET: api/Image
        public JsonResult Get()
        {
            return Json(new List<MediaModel>
            {
                new MediaModel()
                {
                    Data = new ImageToAsciiConverter().GetArrayImage(Server.MapPath("~/Content/test_large.jpg"), 100)
                }
            }, JsonRequestBehavior.AllowGet);
        }

        //// GET: api/Image/5
        //public MediaModel Get(int id)
        //{
        //    return new MediaModel();
        //}

        //// POST: api/Image
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Image/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Image/5
        //public void Delete(int id)
        //{
        //}
    }
}
