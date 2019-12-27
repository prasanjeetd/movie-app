using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    [JsonConverter(typeof(FlattenNestedJSONConverter<Movie>))]
    public class Movie
    {
        public int Year { get; set; }
        public string Title { get; set; }

        [JsonProperty("info/image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("info/directors")]
        public string[] Directors { get; set; }
        [JsonProperty("info/release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty("info/genres")]
        public string[] Genres { get; set; }
        [JsonProperty("info/plot")]
        public string Plot { get; set; }
        [JsonProperty("info/rank")]
        public int Rank { get; set; }
        [JsonProperty("info/rating")]
        public double Rating { get; set; }
        [JsonProperty("info/running_time_secs")]
        public long RunningTimeSecs { get; set; }
        [JsonProperty("info/actors")]
        public string[] Actors { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {

        }

    }

}
