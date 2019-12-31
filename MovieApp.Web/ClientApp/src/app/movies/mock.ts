import { Injectable, InjectionToken } from "@angular/core";
import { Movie } from "./movie";
import { of } from "rxjs/observable/of";
import { async as _async } from "rxjs/scheduler/async";

@Injectable()
export class MockMovieApiService {

  getMovies() {

    const movie1 = {} as Movie;
    movie1.title = "movie1";
    movie1.year = 2019;

    const movie2 = {} as Movie;
    movie2.title = "movie2";
    movie2.year = 2020;
    const movies = [
      movie1,
      movie2
    ] as Movie[];

    return of(movies);
    // return Promise.resolve(movies);

  };

  findMovies(title) {

    console.log("title:", title);

    const movie = {} as Movie;
    movie.title = "test";
    movie.year = 2019;

    const movies = [
      movie
    ] as Movie[];

    return of(movies, _async);
  }

};

export const BASE_URL = new InjectionToken<string>('baseurl');
