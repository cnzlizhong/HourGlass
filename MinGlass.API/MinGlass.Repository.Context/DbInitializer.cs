using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Repository.Context
{
    public static class DbInitializer
    {
        public static async void Initialize(MigrateAppContext context)
        {
            context.Database.Migrate();

            await context.SaveChangesAsync();
        }
    }
}
