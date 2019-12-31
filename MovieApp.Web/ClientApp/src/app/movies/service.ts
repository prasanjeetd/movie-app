import { Injectable, Inject } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';

import { Movie } from "./movie";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class MovieApiService {

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string) {
  }

  getMovies(): Observable<Movie[]> {

    return this.http.get<Movie[]>(this.baseUrl + 'api/movie').pipe(
      catchError(this.handleError<Movie[]>('getMovies', []))
    );

  }

  findMovies(title): Observable<Movie[]> {
    return this.http.post<Movie[]>(this.baseUrl + 'api/movie', { title: title }).pipe(
      catchError(this.handleError<Movie[]>('findMovies', []))
    );
  }

  getMovie(title: string): Observable<Movie> {

    return this.http.get<Movie>(this.baseUrl + 'api/movie/' + title).pipe(
      catchError(this.handleError<Movie>('getMovie', {} as Movie))
    );

  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
