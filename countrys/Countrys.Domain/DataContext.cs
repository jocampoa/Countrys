namespace Countrys.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Countrys.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Countrys.Domain.UserType> UserTypes { get; set; }
    }
}
