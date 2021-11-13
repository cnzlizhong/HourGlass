using Microsoft.EntityFrameworkCore;
using MinGlass.Models;
using MinGlass.Repository.Context;
using MinGlass.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Repository
{
    public class UserRepository : BaseEFRepository<User>, IUserRepository
    {
        private readonly ClientAppContext _context;
        public UserRepository(ClientAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            Add(user);

            await SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await GetQuery().SingleOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await GetQuery().SingleOrDefaultAsync(user => user.Id == id);
        }
    }
}
