
namespace STS.SharedKernel.Exceptions
{
    public class STSException : Exception
    {
        public STSException():base(){}
        public STSException(string? message):base(message){}
        public STSException(string message, Exception inner): base(message, inner){}

    }
}
