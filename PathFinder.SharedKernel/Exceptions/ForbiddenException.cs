﻿

namespace STS.SharedKernel.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base() { }
        public ForbiddenException(string? message) : base(message) { }
    }
}
