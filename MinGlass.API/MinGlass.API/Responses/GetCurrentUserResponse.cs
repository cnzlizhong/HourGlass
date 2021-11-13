using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinGlass.API.Responses
{
    public class GetCurrentUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
