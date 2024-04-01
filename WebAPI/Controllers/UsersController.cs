using Microsoft.AspNetCore.Mvc;
using Application.Interface;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _user;

        public UsersController(IUsers user)
        {
            _user = user;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto input)
        {
            var response = await _user.Create(input);

            if (response.Response == StatusCodes.Status201Created.ToString())
            {
                return Created("Ok", response);
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

        // GET: api/<Users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _user.Get(id);

            if (response.Response == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            if (response.Response == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(response);
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

        // GET api/<Users>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _user.GetAll();

            if (response.Response == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            if (response.Response == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(response);
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

        // PUT api/<Users>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto input)
        {
            var response = await _user.Update(input);

            if (response.Response == StatusCodes.Status201Created.ToString())
            {
                return Created("Ok", response);
            }
            if (response.Response == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(response);
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

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _user.Delete(id);

            if (response.Response == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            if (response.Response == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(response);
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
