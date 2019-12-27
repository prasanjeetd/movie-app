using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetN(int offset);

        Movie Get(string title);
    }
}
