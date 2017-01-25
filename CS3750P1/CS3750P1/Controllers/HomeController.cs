using CS3750P1.Models;
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

        //**********************************************************
        //**********************************************************
        //********************* LIST FUNCTIONS**********************
        //**********************************************************
        //**********************************************************
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


        //**********************************************************
        //**********************************************************
        //********************* ITEM FUNCTIONS**********************
        //**********************************************************
        //**********************************************************
        public ActionResult ViewEditList(string name = "", string button = "", int itemID = 0, string itemName = "")
        {
            int listID = 0;
            string id = (string)Url.RequestContext.RouteData.Values["id"];
            int.TryParse(id, out listID);
            using (ToDoContext db = new ToDoContext())
            {
                if (button == "completeItem")
                {
                    Item tempItem = db.Items.Where(x => x.itemID == itemID).Single();
                    tempItem.isCompleted = true;
                }


                var list = db.Lists.Where(x => x.listID == listID).Single();
                ViewBag.listName = list.listName;
                ViewBag.listID = listID;

                db.SaveChanges();
                //create list of items based on listID
                List<Item> itemList = db.Items.Where(x => x.listID == listID).ToList();

                ViewBag.Items = itemList;
            }


        //**********************************************************
        //**********************************************************
        //********************* CATEGORY FUNCTIONS**********************
        //**********************************************************
        //**********************************************************


            using (var ctx = new ToDoContext())
            {
                foreach (List l in ctx.Lists)
                {
                    if (l.listID == listID)
                    {
                        @ViewBag.listName = l.listName;
                    }
                }
            }
            // listID = 2;
            switch (button)
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
                            Category newCat = new Category() { categoryName = name };
                            ctx.Categories.Add(newCat);
                        }
                        ctx.SaveChanges();
                    }
                    // return View();
                    break;
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
                    break;
                //return View(model);
                default:
                    break;
                    //return View(model);

            }

            //Create a list of CategorySelection objects that have a category and a bool selected to 
            //populate with all the categories and whether or not they are selected
            List<CategorySelection> catSelect = new List<CategorySelection>();
            var catsForList = Db.CategoryLists.Where(x => x.listID == listID);

            foreach (var category in Db.Categories)
            {
                var tempCatSelect = new CategorySelection();
                tempCatSelect.category = category;
                tempCatSelect.selected = false;
                var catSelected = catsForList.Where(x => x.categoryID == category.categoryID);
                if (catSelected.Count() > 0)
                {
                    tempCatSelect.selected = true;
                }
                catSelect.Add(tempCatSelect);
            }

            ViewBag.categories = catSelect;

            /*
            foreach (CategoryList cl in Db.CategoryLists)
            {
                if (cl.listID == listID)
                {
                    System.Diagnostics.Debug.WriteLine("Removing these category id's: " + cl.categoryID);

                    Db.CategoryLists.Remove(cl);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Didn't make it in currentListID???");
                    System.Diagnostics.Debug.WriteLine("Current list ID: " + listID);
                    System.Diagnostics.Debug.WriteLine("cl's list ID: " + cl.listID);

                }
            }
            
            Db.SaveChanges();

            // Process according to your requirements:
            foreach (var categorytwo in selectedCategory)
            {
                System.Diagnostics.Debug.WriteLine("adding these category id's " + categorytwo.categoryID + " to this listID: " + listID);

                CategoryList temp = new CategoryList();
                temp.categoryID = categorytwo.categoryID;
                temp.listID = listID;

                Db.CategoryLists.Add(temp);// category.categoryID, currentListID);

                //System.Diagnostics.Debug.WriteLine(category.categoryName);
                //string.Format("{0} {1}", person.firstName, person.LastName));
            }

            Db.SaveChanges();
            */
            return View("ListItems");
            //return View(model);
        }

    }
}