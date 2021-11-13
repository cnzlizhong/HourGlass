using MediatR;
using MinGlass.API.Requests;
using MinGlass.Models;
using MinGlass.Models.Exceptions;
using MinGlass.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MinGlass.API.UseCases
{
    public class RegisterUserUseCase : IRequestHandler<RegisterUserRequest, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var userData = request.Data;
            var existingUser = await _userRepository.GetUserByEmailAsync(userData.Email);
            if (existingUser != null)
                throw new MinglassException("User with this email already exists");

            //TODO: Add validation for user info.

            var user = new User
            {
                Id = new Guid(),
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(userData.Password)
            };

            await _userRepository.CreateUserAsync(user);

            return user.Id;
        }
    }
}
