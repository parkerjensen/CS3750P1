namespace CS3750P1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("List")]
    public partial class List
    {
        public int listID { get; set; }

        [StringLength(50)]
        public string listName { get; set; }

        public DateTime? dateCompleted { get; set; }

        public bool? isCompleted { get; set; }
    }
}
