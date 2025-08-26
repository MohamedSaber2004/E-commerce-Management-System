
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected string GetEmailFromToken() => User.FindFirst(ClaimTypes.Email)?.Value;
    }
}
