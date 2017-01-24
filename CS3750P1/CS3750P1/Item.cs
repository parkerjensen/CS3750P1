namespace CS3750P1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        public int itemID { get; set; }

        [StringLength(500)]
        public string itemName { get; set; }

        public DateTime? dateCompleted { get; set; }

        public bool? isCompleted { get; set; }

        public int listID { get; set; }
    }
}
