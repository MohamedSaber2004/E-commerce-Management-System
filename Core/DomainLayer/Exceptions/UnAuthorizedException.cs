
namespace Domain_Layer.Exceptions
{
    public sealed class UnAuthorizedException(string Message = "Invalid Email Or Password") : Exception(Message)
    {
    }
}
