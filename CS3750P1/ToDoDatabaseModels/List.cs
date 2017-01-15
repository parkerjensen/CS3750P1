using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDatabaseModels
{
    public class List
    {
        public int listID { get; set; }
        public string listName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
