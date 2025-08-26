
namespace Domain_Layer.Exceptions
{
    public sealed class UserNotFoundException(string Email) : NotFoundException($"User With Email {Email} Is Not Found")
    {
    }
}
