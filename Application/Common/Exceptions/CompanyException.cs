using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class CompanyException : Exception {
        public CompanyException(string Message) : base(Message){}
    }
}