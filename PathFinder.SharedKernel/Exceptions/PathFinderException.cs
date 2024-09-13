
namespace PathFinder.SharedKernel.Exceptions
{
    public class PathFinderException : Exception
    {
        public PathFinderException():base(){}
        public PathFinderException(string? message):base(message){}
        public PathFinderException(string message, Exception inner): base(message, inner){}

    }
}
