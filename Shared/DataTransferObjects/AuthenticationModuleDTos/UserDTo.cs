
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.AuthenticationModuleDTos
{
    public class UserDTo
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
