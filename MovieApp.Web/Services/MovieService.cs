using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class MovieService : IMovieService
    {
        IRepository repository;

        public MovieService() { }

        public MovieService(IRepository repository)
        {
            this.repository = repository;
        }

        public Movie Get(string title)
        {
            var movie = this.repository.movies
               .FirstOrDefault(x =>
               x.Title.ToLower().Equals(title.ToLower())
               );

            return movie;
        }

        public IEnumerable<Movie> GetN(int offset)
        {
            var movies = this.repository.movies
               .OrderByDescending(x => x.Year)
               .ThenBy(x => x.Rating)
               .Where(x => !String.IsNullOrEmpty(x.ImageUrl))
               .Take(offset);

            return movies;
        }

        public ICollection<Movie> Find(string title, int offset = 4)
        {
            var movies = this.repository.movies
               .OrderBy(x => x.Title)
               .ThenBy(x => x.Rating)
               .Where(x => x.Title.StartsWith(title, StringComparison.InvariantCultureIgnoreCase))
               .Take(offset);

            return movies.ToList();
        }
    }
}
