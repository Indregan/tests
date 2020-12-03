using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Mvc;

namespace ProductsAPI.Controllers
{
    public class UsersJasonController : Controller
    {
        //C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json

        public IActionResult Index()
        {
            var webClient = new WebClient();

            var jason = webClient.DownloadString(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json");

            var users = JsonConvert.DeserializeObject<UsersJason>(jason);

            return (IActionResult)View(users); //cast
        }
    }
}
