﻿
namespace Application.Common.Exceptions
{
    public  class InvalidLoginException : Exception
    {
        public InvalidLoginException(string errors  ) : base(errors)
        {

        }
        
    }
}
