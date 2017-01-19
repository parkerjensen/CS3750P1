namespace CS3750P1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ToDoContext : DbContext
    {
        public ToDoContext()
            : base("name=ToDoContext1")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryList> CategoryLists { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<List> Lists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
