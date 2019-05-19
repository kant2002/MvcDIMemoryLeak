using MvcDIMemoryLeak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDIMemoryLeak.Controllers
{
    public class HomeController : Controller
    {
        private readonly DummyService dummyService;

        public HomeController(DummyService dummyService)
        {
            this.dummyService = dummyService;
        }

        public ActionResult Index()
        {
            return View(this.dummyService.Id);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}