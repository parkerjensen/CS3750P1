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

        public ActionResult ViewLists()
        {
            ToDoContext db = new ToDoContext();
            ViewBag.Message = "This is the list view page.";
            ViewBag.List = null;
            ViewBag.Category = null;
            return View();
        }

        public ActionResult ListAll()
        {
            ToDoContext db = new ToDoContext();
            ViewBag.Message = "This is the list view page showing all of the lists.";
            ViewBag.List = new List<List>(db.Lists);
            ViewBag.Category = "All";
            return View("ViewLists");
        }

        [HttpPost]
        public ActionResult ListByCategory(string catSubmit)
        {
            if (catSubmit != null && catSubmit != "")
            {
                ToDoContext db = new ToDoContext();
                ViewBag.Message = "This is the list view page showing all of the lists of the category: " + catSubmit + ".";
                //get list id
                int catId = -1;
                foreach (Category c in db.Categories)
                {
                    if (c.categoryName == catSubmit)
                    {
                        catId = c.categoryID;
                    }
                }
                //create list of lists based on category
                List<List> catList = new List<List>();
                foreach (CategoryList cl in db.CategoryLists)
                {
                    if (cl.categoryID == catId)
                    {
                        foreach (List l in db.Lists)
                        {
                            if (l.listID == cl.listID)
                            {
                                catList.Add(l);
                            }
                        }
                    }
                }
                ViewBag.List = catList;
                ViewBag.Category = catSubmit;
                return View("ViewLists");
            }
            else
            {
                return RedirectToAction("ListAll");
            }
        }
    }
}