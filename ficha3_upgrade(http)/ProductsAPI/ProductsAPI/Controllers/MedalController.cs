using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ProductsAPI.Controllers
{
    public class MedalController : Controller
    {
        public ActionResult Index()
        {
            string medalsXML = System.IO.File.ReadAllText(Server.MapPath("Data/Medals.xml"));
            ViewBag.Medals = medalsXML;

            return View();
        }
    }
}
