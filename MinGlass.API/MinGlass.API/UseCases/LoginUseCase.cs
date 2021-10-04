using MediatR;
using MinGlass.API.Requests;
using MinGlass.API.services;
using MinGlass.Models.Exceptions;
using MinGlass.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MinGlass.API.UseCases
{
    public class LoginUseCase : IRequestHandler<LoginRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginUseCase(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var loginData = request.data;
            var user = await _userRepository.GetUserByEmail(loginData.Email);
            if (user == null)
                throw new MinglassUnauthorizationException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
                throw new MinglassUnauthorizationException("Invalid credentials");

            var jwtToken = _jwtService.GenerateToken(user.Email, user.Id.ToString(), user.FirstName, user.LastName);

            return jwtToken;
        }
    }
}
