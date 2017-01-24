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

        ToDoContext Db = new ToDoContext();

        

        public ActionResult AddCategory(string name, string addOrDeleteCategoryButton)
        {
            //@ViewBag.message = name; //"You got into AddCategory";
            @ViewBag.deletemessage = addOrDeleteCategoryButton;
            @ViewBag.addmessage = addOrDeleteCategoryButton;

            switch (addOrDeleteCategoryButton)
            {
                case "addCat":
                    @ViewBag.message = "You added a Category:" + name;
                    using (var ctx = new ToDoContext())
                    {
                        bool inDB = false;
                        // List test = new List() { listName = "Test List" };
                        foreach (Category c in ctx.Categories)
                        {
                            if (c.categoryName == name)
                            {
                                inDB = true;
                                @ViewBag.message = "Your Category " + name + " is already in the Database, Query was not sent.";
                            }
                        }
                        if (!inDB)
                        {
                            Category catTest = new Category() { categoryName = name };
                            ctx.Categories.Add(catTest);
                        }
                        ctx.SaveChanges();
                    }
                    return View();
                case "delCat":
                    @ViewBag.message = "You deleted a Category:" + name;
                    using (var ctx = new ToDoContext())
                    {
                        // List test = new List() { listName = "Test List" };
                       // Category catTest = new Category() { categoryName = name };

                        foreach (Category c in ctx.Categories)
                        {
                            if (c.categoryName == name)
                            {
                                ctx.Categories.Remove(c);
                                
                            }
                        }
                        // ctx.Categories.Remove(name);
                        ctx.SaveChanges();
                    }
                    return View();
                default:
                    return View();
                  
            }


            return View();
            //switch (submitButton)
            //{
                
            //    case "AddC":
            //        // delegate sending to another controller action
            //        @ViewBag.message = "You added a Category";
            //        return View();

            //    case "DeleteC":
            //        // call another action to perform the cancellation
            //        @ViewBag.message = "You deleted a Category";
            //        return View();

            //    default:
            //        // If they've submitted the form without a submitButton, 
            //        // just return the view again.
            //        return (View());
            //}
        }

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
            ViewBag.Message = "Hello Brady";

            return View();
        }

        public ActionResult BradyPage()
        {
            ViewBag.Message = "Hello Brady";

            ViewBag.Message = "I'm glad you learned how to make your own page";

            using (var ctx = new ToDoContext())
            {


                //ViewBag.firstCategoryItem = ctx.Categories.LastOrDefault().categoryID;

                ctx.SaveChanges();
            }

            //using (var ctx = new ToDoContext())
            //{
            //    List test = new List() { listName = "Test List" };

            //    ctx.Lists.Add(test);
            //    ctx.SaveChanges();
            //}
            ViewBag.MyMessageToUsers = "Hello from me.";
            ViewBag.CategoryText = "Your New Category Goes Here.";
            ViewBag.CheckTheBox = true;
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