using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IDBSource dbSource;

        public MovieController(IDBSource dbSource)
        {
            this.dbSource = dbSource;
        }

        [HttpGet("{pageSize:int?}")]
        public ActionResult<IEnumerable<Movie>> GetAll(int pageSize = 4)
        {
            var result = this.dbSource.movies
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Rating)
                .Where(x => !String.IsNullOrEmpty(x.ImageUrl))
                .Take(pageSize);

            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpGet("{title}")]
        public ActionResult<IEnumerable<Movie>> Get(string title)
        {
            var result = this.dbSource.movies
                .Where(x => x.Title.ToLower().Equals(title.ToLower()));

            return StatusCode(StatusCodes.Status200OK, result);

        }
    }
}