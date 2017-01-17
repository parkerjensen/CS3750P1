using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDatabaseModels
{
    public class Item
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        //public string itemDescription { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? dateCompleted { get; set; }
    }
}
