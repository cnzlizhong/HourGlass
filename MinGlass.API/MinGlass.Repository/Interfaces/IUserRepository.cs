using MinGlass.Models;
using System;
using System.Threading.Tasks;

namespace MinGlass.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
