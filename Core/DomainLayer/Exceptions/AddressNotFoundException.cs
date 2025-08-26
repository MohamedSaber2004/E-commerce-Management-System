
namespace Domain_Layer.Exceptions
{
    public sealed class AddressNotFoundException(string email) : NotFoundException($"User With Email {email} Has No Address")
    {
    }
}
