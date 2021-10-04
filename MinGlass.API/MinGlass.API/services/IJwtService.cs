using MinGlass.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MinGlass.API.services
{
    public interface IJwtService
    {
        string GenerateToken(string email, string id, string firstName, string lastName);
        JwtSecurityToken Verify(string jwtToken);
    }
}
