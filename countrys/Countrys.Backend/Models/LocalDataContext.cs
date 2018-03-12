namespace Countrys.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Countrys.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Countrys.Domain.UserType> UserTypes { get; set; }
    }
}