using MinGlass.Models;
using System.Threading.Tasks;

namespace MinGlass.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
    }
}
