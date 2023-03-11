using DTO.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.AuthenticationRepository;

namespace OnlineBookingTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthenRequest request)
        {
            var user = await _authenticationRepository.Login(request);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationRepository.Register(request));
        }
    }
}
