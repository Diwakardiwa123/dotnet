using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace eCommerce
{
    public class HomeController : Controller
    {
        public UserProfileModel resultModel;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(object model)
        {
            return View();
        }
    }
}