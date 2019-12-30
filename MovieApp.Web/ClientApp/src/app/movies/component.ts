import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { MovieApiService } from "./service";
import { Movie } from "./movie";
import { Subject } from "rxjs/Subject";
import { from } from 'rxjs/observable/from';
import { of } from 'rxjs/observable/of';
import { switchMap, debounceTime, distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'movie-home',
  templateUrl: 'component.html',
  styleUrls: ['style.css'],
  providers: [MovieApiService]
})
export class MovieHomeComponent implements OnInit {

  movies$: Observable<Movie[]>;
  searchedMovies$: Observable<Movie[]>;

  private searchTerms = new Subject<string>();

  constructor(private movieApiService: MovieApiService) {

    this.movies$ = this.movieApiService.getMovies();
  }

  ngOnInit(): void {

    this.searchedMovies$ = this.searchTerms.pipe(

      debounceTime(200),

      distinctUntilChanged(),

      switchMap((title: string) =>
       this.movieApiService.findMovies(title)
      )
    );

    this.searchedMovies$.subscribe((movies) =>
      this.movies$ = of(movies)
    )
  }

  search(title: string): void {

    if (title === "") {
      this.movies$ =  this.movieApiService.getMovies();
    }
    else {
      this.searchTerms.next(title);
    }


  }

}
