using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public interface IRepository
    {
        List<Movie> movies { get; set; }
    }

    public class JsonRespository :  IRepository
    {
        public List<Movie> movies { get; set; }

        public string a;

        public JsonRespository()
        {
            movies = new List<Movie>();

            this.Seed();
        }

        private void Seed()
        {
            var dataFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "moviedata.json");

            var json = System.IO.File.ReadAllText(dataFile);

            //List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json, new FlattenNestedJSONConverter<Movie>());

            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);

            this.movies = movies;

        }
    }
}
