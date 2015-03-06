namespace ConsoleApp1
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AppDBContext : DbContext
    {
        // Your context has been configured to use a 'App1DBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.App1DBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'App1DBContext' 
        // connection string in the application configuration file.

        public AppDBContext()
            : base("name=App1DBContext")
        {
            // this.Configuration.LazyLoadingEnabled = false;          
          
            Database.SetInitializer<AppDBContext>(new DataInitializer());
        }

        public DbSet<AppClass> AppClasses { get; set; }
        public DbSet<AppStudent> AppStudent { get; set; }
        //public DbSet<AppCompany> AppCompanies { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}