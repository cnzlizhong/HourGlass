using MediatR;
using MinGlass.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinGlass.API.Requests
{
    public class LoginRequest : IRequest<string>
    {
        public LoginRequest(LoginDto data)
        {
            this.data = data;
        }

        public LoginDto data { get; }
    }
}
