using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Models.Exceptions
{
    public class MinglassUnauthorizationException : MinglassException
    {
        public MinglassUnauthorizationException()
        {
        }

        public MinglassUnauthorizationException(string message) : base(message)
        {
        }

        public MinglassUnauthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
