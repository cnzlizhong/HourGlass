using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Models.Exceptions
{
    public class MinglassUnauthorizedException : MinglassException
    {
        private static string DEFAULT_MESSAGE = "Access denied";
        public MinglassUnauthorizedException() : base(DEFAULT_MESSAGE)
        {
        }

        public MinglassUnauthorizedException(string message) : base(string.IsNullOrWhiteSpace(message) ? DEFAULT_MESSAGE : message)
        {
        }

        public MinglassUnauthorizedException(string message, Exception innerException) : base(string.IsNullOrWhiteSpace(message) ? DEFAULT_MESSAGE : message, innerException)
        {
        }
    }
}
