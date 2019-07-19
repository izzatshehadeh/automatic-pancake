using izBackend.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace izBackend.Models
{
    public class IzDbContext : DbContext
    {

       
        public IzDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            System.Collections.Generic.List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreationTime").CurrentValue = DateTime.Now;
            });

            System.Collections.Generic.List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedDate").CurrentValue = DateTime.Now;
            });

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

    }
}
