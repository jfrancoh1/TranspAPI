using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly IBranches _branch;

        public BranchesController(IBranches branch)
        {
            _branch = branch;
        }

        [HttpPost]

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateBranchDto input)
        {
            var response = await _branch.Create(input);

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

        // GET: api/<Branches>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _branch.Get(id);

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

        // GET api/<Branches>
        [HttpGet]

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _branch.GetAll();

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

        // PUT api/<Branches>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBranchDto input)
        {
            var response = await _branch.Update(input);

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

        // DELETE api/<Branches>/5
        [HttpDelete("{id}")]

        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _branch.Delete(id);

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
