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
    public class CategoryController : Controller
    {
        ToDoContext Db = new ToDoContext();
        //int currentListID;

        //took out listID as first parameter because it can't be null. wait until we have it linked up to add back
        public ActionResult CategoryIndex(string name, string addOrDeleteCategoryButton, CategorySelectionViewModel modelButton, int listID=2)
        {
            //int listID = 2;
            //currentListID = listID;

            using (var ctx = new ToDoContext())
            {
                foreach(List l in ctx.Lists)
                {
                    if (l.listID == listID)
                    {
                        @ViewBag.listName = l.listName;
                    }
                }
            }
                // listID = 2;
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
            var model = new CategorySelectionViewModel();
            foreach (var category in Db.Categories)
            {

                var editorViewModel = new SelectCategoryEditorViewModel()
                {
                    // if (category.categoryID == listID)
                    // {
                    //listIDforButton = currentListID,
                    id = category.categoryID,
                    categoryName = category.categoryName, // string.Format("{0} {1}", category.categoryName),
                    Selected = false,
                    listIDforButton = listID
                    // }
                };
                //editorViewModel.Selected = true;
                foreach (CategoryList cl in Db.CategoryLists)
                {
                    if (cl.categoryID == category.categoryID && cl.listID == listID) //listID == listID)
                    {
                        editorViewModel.Selected = true;
                    }
                }

                model.Category.Add(editorViewModel);

            }

            // get the ids of the items selected:
            var selectedIds = modelButton.getSelectedIds();
            // Use the ids to retrieve the records for the selected people
            // from the database:
            var selectedCategory = from x in Db.Categories
                                    where selectedIds.Contains(x.categoryID)
                                    select x;


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

            return View(model);
            //return View(model);
        }


//        [HttpPost]
//        public ActionResult SubmitSelected(CategorySelectionViewModel model)
//        {
//            // get the ids of the items selected:
//            var selectedIds = model.getSelectedIds();
//            // Use the ids to retrieve the records for the selected people
//            // from the database:
//            var selectedCategory = from x in Db.Categories
//                                 where selectedIds.Contains(x.categoryID)
//                                 select x;


//            foreach (CategoryList cl in Db.CategoryLists)
//            {
//                if (cl.listID == currentListID)
//                {
//                    System.Diagnostics.Debug.WriteLine("Helloasdfasdfasdfasdfasdfasdfasd");
//                    Db.CategoryLists.Remove(cl);
//                }
//                else
//                { 
//                    System.Diagnostics.Debug.WriteLine("Didn't make it in currentListID???");
//                    System.Diagnostics.Debug.WriteLine("Current list ID: " + currentListID);
//                    System.Diagnostics.Debug.WriteLine("cl's list ID: " + cl.listID);

//                }
//            }

//            // Process according to your requirements:
//            foreach (var category in selectedCategory)
//            {


//                CategoryList temp = new CategoryList();
//                temp.categoryID = category.categoryID;
//                temp.listID = currentListID;

//                Db.CategoryLists.Add(temp);// category.categoryID, currentListID);
             
//                //System.Diagnostics.Debug.WriteLine(category.categoryName);
//                    //string.Format("{0} {1}", person.firstName, person.LastName));
//            }
//            // Redirect somewhere meaningful (probably to somewhere showing 
//            // the results of your processing):
//            return RedirectToAction("CategoryIndex");
//        }
    }
}