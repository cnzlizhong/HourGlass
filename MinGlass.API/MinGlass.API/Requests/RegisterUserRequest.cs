using MediatR;
using MinGlass.API.Dtos;
using System;

namespace MinGlass.API.Requests
{
    public class RegisterUserRequest : IRequest<Guid>
    {
        public RegisterUserRequest(RegisterUserDto data)
        {
            Data = data;
        }

        public RegisterUserDto Data { get; }
    }
}
