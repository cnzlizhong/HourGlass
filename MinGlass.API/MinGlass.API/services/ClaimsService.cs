using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace MinGlass.API.services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var claim = GetClaim("userId");
            return claim == null ? null : claim.Value;
        }

        private Claim GetClaim(string claimType)
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new CookieException();

            var claim = _httpContextAccessor.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == claimType);

            return claim;
        }
    }
}
