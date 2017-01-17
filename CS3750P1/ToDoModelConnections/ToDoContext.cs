using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ToDoDatabaseModels;

namespace ToDoModelConnections
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base()
        {

        }

        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
