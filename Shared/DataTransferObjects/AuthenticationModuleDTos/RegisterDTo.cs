using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.AuthenticationModuleDTos
{
    public class RegisterDTo
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? UserName { get; set; } = "MohamedSaber";
        public string DisplayName { get; set; } = default!;
        [Phone] 
        public string? PhoneNumber { get; set; }
    }
}
