using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //IEnumerable<ViewModelinventory> 
        public ActionResult Index()
        {
            return View();           
        }
    }
}