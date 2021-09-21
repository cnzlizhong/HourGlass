using Microsoft.EntityFrameworkCore;
using MinGlass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Repository.Context.Configurations
{
    internal static class UserConfig
    {
        public static ModelBuilder ConfigureUsers(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();

            entity.ToTable("Users").HasKey(user => user.Id);

            entity.Property(user => user.Email).IsRequired();

            entity.HasIndex(user => user.Email).IsUnique();

            entity.Property(user => user.Password).IsRequired();

            entity.Property(user => user.FirstName).IsRequired();

            entity.Property(user => user.LastName).IsRequired();

            return modelBuilder;
        }
    }
}
