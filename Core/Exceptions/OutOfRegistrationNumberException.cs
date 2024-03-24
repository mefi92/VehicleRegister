
namespace Core.Exceptions
{
    public class OutOfRegistrationNumberException : Exception
    {
        public OutOfRegistrationNumberException(string message, Exception innerException) : base(message, innerException) { }
    }
}
