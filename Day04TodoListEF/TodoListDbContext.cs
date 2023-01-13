using System;
using System.Data.Entity;
using System.Linq;

namespace Day04TodoListEF
{
    public class TodoListDbContext : DbContext
    {
        // Your context has been configured to use a 'TodoListDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Day04TodoListEF.TodoListDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TodoListDbContext' 
        // connection string in the application configuration file.
        public TodoListDbContext()
            : base("name=TodoListDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}