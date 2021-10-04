using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Models.Exceptions
{
    public class MinglassException : Exception
    {
        public MinglassException()
        {
        }

        public MinglassException(string message) : base(message)
        {
        }

        public MinglassException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
