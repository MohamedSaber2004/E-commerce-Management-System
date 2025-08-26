
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DataTransferObjects.AuthenticationModuleDTos;
using System.Security.Claims;

namespace Presentation_Layer.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDTo loginDTo)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDTo);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDTo registerDTo)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDTo);
            return Ok(user);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTo>> GetCurrentUser()
        {
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken());
            return Ok(user);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTo>> GetCurrentUserAddress()
        {
            var userAddress = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken());
            return Ok(userAddress);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTo>> UpdateCurrentUserAddress(AddressDTo addressDTo)
        {
            var updatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(addressDTo, GetEmailFromToken());
            return Ok(updatedAddress);
        }
    }
}
