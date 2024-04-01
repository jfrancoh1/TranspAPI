using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = await _authentication.GetToken(login);

            if (response.Response == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            if (response.Response == StatusCodes.Status401Unauthorized.ToString())
            {
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            if (response.Response == StatusCodes.Status500InternalServerError.ToString())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
