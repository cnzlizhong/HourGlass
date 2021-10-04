using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.API.services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string SECURITY_KEY;
        private readonly string ISSUER;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            SECURITY_KEY = _configuration.GetValue<string>("jwtSecurityKey");
            ISSUER = _configuration.GetValue<string>("jwtIssuer");
        }

        public string GenerateToken(string email, string id, string firstName, string lastName)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECURITY_KEY));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var claims = new List<Claim>
            {
                new Claim("email", email),
                new Claim("userId", id),
                new Claim("firstName", firstName),
                new Claim("lastName", lastName)
            };

            var payload = new JwtPayload(ISSUER, ISSUER, claims, null, DateTime.Today.AddHours(1));

            var securityToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(securityToken);

            return token;
        }

        public JwtSecurityToken Verify(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECURITY_KEY);

            handler.ValidateToken(jwtToken, 
                new TokenValidationParameters() { 
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = ISSUER,
                    ValidAudience = ISSUER,
                    RequireExpirationTime = true
                }, 
                out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
