using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.AuthenticationModuleDTos
{
    public class LoginDTo
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
