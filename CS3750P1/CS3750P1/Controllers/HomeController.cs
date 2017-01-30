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
        
        public ActionResult ViewEditList()
        {
            var model = new EditListModel();
            ;
            int listID = 0;
            string id = (string)Url.RequestContext.RouteData.Values["id"];
            int.TryParse(id, out listID);

            using (ToDoContext db = new ToDoContext())
            {
                model.listName = db.Lists.Where(x => x.listID == listID).Single().listName;
                model.listID = listID;
                //create list of items based on listID
                model.items = db.Items.Where(x => x.listID == listID).ToList();
                var selectedCats = db.CategoryLists.Where(x => x.listID == listID).Select(y => y.categoryID);
                model.categories = new List<CategorySelect>();
                foreach (Category cat in db.Categories)
                {
                    var tempCat = new CategorySelect();
                    tempCat.id = cat.categoryID;
                    tempCat.categoryName = cat.categoryName;
                    if (selectedCats.Contains(cat.categoryID))
                    {
                        tempCat.Selected = true;
                    }
                    else
                    {
                        tempCat.Selected = false;
                    }
                    model.categories.Add(tempCat);
                }
            }
            return View("ListItems", model);
        }
        
        [HttpPost]
        public ActionResult EditItems(EditListModel model, string button, string newCat = "", string newItem = "")
        {
            ViewBag.CatMessage = "";
            using(ToDoContext db = new ToDoContext())
            {
                if (button == "Update")
                {
                    for (var i = 0; i < model.items.Count(); i++)
                    {
                        db.Entry(model.items[i]).State = System.Data.Entity.EntityState.Modified;
                        
                    }
                    for (var i = 0; i < model.categories.Count(); i++)
                    {
                        if (model.categories[i].Selected == true)
                        {
                            var catID = model.categories[i].id;
                            var listID = model.listID;
                            if(db.CategoryLists.Where(x => x.categoryID == catID && x.listID == listID).Count() < 1)
                            {
                                var tempCatList = new CategoryList();
                                tempCatList.categoryID = model.categories[i].id;
                                tempCatList.listID = model.listID;
                                db.CategoryLists.Add(tempCatList);
                            }
                        }
                        else
                        {
                            var catID = model.categories[i].id;
                            var listID = model.listID;
                            if (db.CategoryLists.Where(x => x.categoryID == catID && x.listID == listID).Count() > 0)
                            {
                                db.CategoryLists.Remove(db.CategoryLists.Where(x => x.categoryID == catID && x.listID == listID).First());
                            }
                        }
                    }
                    
                }
                else if (button == "NewCat")
                {

                    if (db.Categories.Where(x => x.categoryName == newCat).Count() > 0)
                    {
                        ViewBag.CatMessage = "That category already exists.";
                    }
                    else
                    {
                        Category Cat = new Category();
                        Cat.categoryName = newCat;
                        db.Categories.Add(Cat);
                        db.SaveChanges();
                        var selectedCats = db.CategoryLists.Where(x => x.listID == model.listID).Select(y => y.categoryID);
                        model.categories = new List<CategorySelect>();
                        foreach (Category cat in db.Categories)
                        {
                            var tempCat = new CategorySelect();
                            tempCat.id = cat.categoryID;
                            tempCat.categoryName = cat.categoryName;
                            if (selectedCats.Contains(cat.categoryID))
                            {
                                tempCat.Selected = true;
                            }
                            else
                            {
                                tempCat.Selected = false;
                            }
                            model.categories.Add(tempCat);
                        }
                    }
                }
                else if(button == "NewItem")
                {
                    Item tempItem = new Item();
                    tempItem.itemName = newItem;
                    tempItem.listID = model.listID;
                    db.Items.Add(tempItem);
                    model.items.Add(tempItem);
                }
                else if (model.changed == "CompleteItem")
                {
                    int completed = 0;
                    int.TryParse(button, out completed);
                    db.Items.Where(x => x.itemID == completed).Single().isCompleted = true;
                    model.items.Where(x => x.itemID == completed).Single().isCompleted = true;
                    db.Items.Where(x => x.itemID == completed).Single().dateCompleted = DateTime.Now;
                    model.items.Where(x => x.itemID == completed).Single().dateCompleted = DateTime.Now;
                }
                else if (model.changed == "DeleteCat")
                {
                    int delete = 0;
                    int.TryParse(button, out delete);
                    db.Categories.Remove(db.Categories.Where(x => x.categoryID == delete).Single());
                    model.categories.Remove(model.categories.Where(x => x.id == delete).Single());
                }
                ModelState.Clear();
                db.SaveChanges();
            }
            return View("ListItems", model);
        }

        /*public ActionResult UpdateCategories(EditListModel model, string button, string newCat = "", string newItem = "")
        {
            ViewBag.CatMessage = "";
            using (ToDoContext db = new ToDoContext())
            {
                if (button == "Update")
                {
                    for (var i = 0; i < model.items.Count(); i++)
                    {
                        db.Entry(model.items[i]).State = System.Data.Entity.EntityState.Modified;

                    }

                }
                if (button == "NewCat")
                {

                    if(db.Categories.Where(x => x.categoryName == newCat).Count() > 0)
                    {
                        ViewBag.CatMessage = "That category already exists.";
                    }
                    else
                    {
                        Category Cat = new Category();
                        Cat.categoryName = newCat;
                        db.Categories.Add(Cat);
                        db.SaveChanges();
                        var selectedCats = db.CategoryLists.Where(x => x.listID == model.listID).Select(y => y.categoryID);
                        model.categories = new List<CategorySelect>();
                        foreach (Category cat in db.Categories)
                        {
                            var tempCat = new CategorySelect();
                            tempCat.id = cat.categoryID;
                            tempCat.categoryName = cat.categoryName;
                            if (selectedCats.Contains(cat.categoryID))
                            {
                                tempCat.Selected = true;
                            }
                            else
                            {
                                tempCat.Selected = false;
                            }
                            model.categories.Add(tempCat);
                        }
                    }
                }
                else
                {
                    int delete = 0;
                    int.TryParse(button, out delete);
                    db.Categories.Remove(db.Categories.Where(x => x.categoryID == delete).Single());
                    model.categories.Remove(model.categories.Where(x => x.id == delete).Single());
                }

                ModelState.Clear();
                db.SaveChanges();
            }
            return View("ListItems", model);
        }
        */
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