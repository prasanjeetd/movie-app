using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Utilities.ExceptionHandling;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IRepository dbSource;

        public MovieController(IRepository dbSource)
        {
            this.dbSource = dbSource;
        }

        [HttpGet("{pageSize:int?}")]
        public ActionResult<IEnumerable<Movie>> GetAll(int pageSize = 4)
        {

            try
            {
                var result = this.dbSource.movies
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Rating)
                .Where(x => !String.IsNullOrEmpty(x.ImageUrl))
                .Take(pageSize);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Message);
            }


        }

        [HttpGet("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Movie> Get(string title)
        {

            try
            {
                var result = this.dbSource.movies
                .FirstOrDefault(x => x.Title.ToLower().Equals(title.ToLower()));

                if (result == null)
                {

                    return NotFound($"No movie found with the title { title}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}