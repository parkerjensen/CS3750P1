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
        public int itemChanged { get; set; }
        public int categoryChanged { get; set; }
    }
}