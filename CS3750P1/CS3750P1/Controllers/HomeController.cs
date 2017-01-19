using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ToDoDatabaseModels;
//using ToDoModelConnections;

namespace CS3750P1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Test()
        {
            using (var ctx = new ToDoContext())
            {
                List test = new List() { listName = "Test List"};

                ctx.Lists.Add(test);
                ctx.SaveChanges();
            }
            ViewBag.Message = "This is a test page.";
            return View();
        }
    }
}