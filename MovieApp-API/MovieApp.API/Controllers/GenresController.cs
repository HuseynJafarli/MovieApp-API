using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.ApiResponses;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions.Common;
using MovieApp.Business.Exceptions.GenreExceptions;
using MovieApp.Business.Services.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _genreService.GetByExpression();

            return Ok(new ApiResponse<ICollection<GenreGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = string.Empty,
                Data = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GenreGetDto? dto = null;
            try
            {
                dto = await _genreService.GetById(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id is not valid",
                    Data = null
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<GenreGetDto>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null,
                Data = dto
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            try
            {
                await _genreService.CreateAsync(dto);
            }
            catch (GenreAlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    Data = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new ApiResponse<GenreGetDto>
            {
                Data = null,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpdateDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
