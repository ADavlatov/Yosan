using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yosan.Auth.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => 
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("");
                entity.Property(x => x.Username).HasColumnName("Username");
                entity.Property(x => x.Email).HasColumnName("Email");
                entity.Property(x => x.Password).HasColumnName("Password");
            });
        }
    }
}