using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Utilities.ExceptionHandling;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService service;

        public MovieController(IMovieService service)
        {
            this.service = service;
        }

        [HttpGet("{pageSize:int?}")]
        public ActionResult<IEnumerable<Movie>> GetAll(int pageSize = 4)
        {

            try
            {
                return StatusCode(StatusCodes.Status200OK, this.service.GetN(pageSize));

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Message);
            }


        }

        [HttpGet("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> Get(string title)
        {

            try
            {
                var movie = this.service.Get(title);

                if (movie == null)
                {
                    return NotFound($"No movie found with the title { title}");
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Movie>> Find(dynamic search)
        {

            try
            {
                if (search == null || search.title == null)
                {
                    return BadRequest($"Invalid search request");
                }

                var movies = this.service.Find(search.title.Value);

                if (movies == null || movies.Count == 0)
                {
                    return NotFound($"No movie found with the title { search}");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Message);
            }


        }
    }
}