using MediatR;
using MinGlass.API.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinGlass.API.Requests
{
    public class GetCurrentUserRequest : IRequest<GetCurrentUserResponse>
    {
    }
}
