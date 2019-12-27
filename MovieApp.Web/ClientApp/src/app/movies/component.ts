import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { MovieApiService } from "./service";
import { Movie } from "./movie";

@Component({
  selector: 'movie-home',
  templateUrl: 'component.html',
  styleUrls: ['style.css'],
  providers: [MovieApiService]
})
export class MovieHomeComponent implements OnInit {

  movies: Observable<Movie[]>;

  constructor(private movieApiService: MovieApiService) {

    this.movies = this.movieApiService.getMovies();
  }

  ngOnInit(): void {
  }


}
