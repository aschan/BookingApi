using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScandicCase.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string s) : base(s)
        {
        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string s) : base(s)
        {
        }
    }
}
