namespace CS3750P1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }
    }
}
