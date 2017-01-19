using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDatabaseModels
{
    public class CategoryList
    {
        public int categoryItemID;
        public virtual ICollection<List> Lists { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
