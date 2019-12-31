import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { MovieApiService } from "../movies/service";
import { Movie } from "../movies/movie";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'movie-detail',
  templateUrl: 'component.html',
  styleUrls: [ "../movies/style.css"],
  providers: [MovieApiService]
})
export class MovieDetailComponent implements OnInit {

  movie: Movie;

  constructor(
    private route: ActivatedRoute,
    private movieApiService: MovieApiService) {
  }

  ngOnInit(): void {

    const title = this.route.snapshot.paramMap.get('title');

    this.movieApiService.getMovie(title)
      .subscribe((movie) => this.movie = movie);

  }


}
