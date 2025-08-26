using Shared.DataTransferObjects.AuthenticationModuleDTos;

namespace Service_Abstraction
{
    public interface IAuthenticationService
    {
        Task<UserDTo> LoginAsync(LoginDTo loginDTo);

        Task<UserDTo> RegisterAsync(RegisterDTo registerDTo);

        Task<bool> CheckEmailAsync(string email);

        Task<AddressDTo> GetCurrentUserAddressAsync(string email);

        Task<AddressDTo> UpdateCurrentUserAddressAsync(AddressDTo addressDTo,string email);

        Task<UserDTo> GetCurrentUserAsync(string email);
    }
}
