using Microsoft.EntityFrameworkCore;
using MinGlass.Models;
using MinGlass.Repository.Context.Configurations;
using System;
using System.Linq;

namespace MinGlass.Repository.Context
{
    public class AppContext: DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("minGlass");

            modelBuilder.ConfigureUsers();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("Created")
                    .HasDefaultValueSql("timezone('utc', now())");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("Modified");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("ModifiedBy");
            }
        }
    }

    public class ClientAppContext : AppContext
    {
        public ClientAppContext(DbContextOptions<ClientAppContext> options) : base(options)
        {
        }

    }

    public class MigrateAppContext : AppContext
    {
        public MigrateAppContext(DbContextOptions<MigrateAppContext> options) : base(options) 
        {
        }
    }
}
