namespace ASP_SIMPLE_CRUD.DbContexts
{
    using ASP_SIMPLE_CRUD.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }

}