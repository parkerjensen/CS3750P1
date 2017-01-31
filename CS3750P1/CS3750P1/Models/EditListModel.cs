using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS3750P1.Models
{
    public class EditListModel
    {
        public int listID { get; set; }
        public string listName { get; set; }
        public List<Item> items { get; set; }
        public List<CategorySelect> categories { get; set; }
        public string changed { get; set; }

        public EditListModel()
        {
            this.listID = 0;
            this.listName = "";
            this.items = new List<Item>();
            this.categories = new List<CategorySelect>();
            this.changed = "";
        }
    }
}