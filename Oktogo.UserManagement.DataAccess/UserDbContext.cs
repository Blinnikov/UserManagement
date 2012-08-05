using System.Data.Entity;

using Oktogo.UserManagement.Entities;

namespace Oktogo.UserManagement.DataAccess
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}