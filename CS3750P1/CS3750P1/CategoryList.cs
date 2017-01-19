namespace CS3750P1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryList")]
    public partial class CategoryList
    {
        public int categoryListID { get; set; }

        public int? categoryID { get; set; }

        public int? listID { get; set; }
    }
}
