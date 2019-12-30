import { Injectable, Inject } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { of } from 'rxjs/observable/of';

import { Movie } from "./movie";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class MovieApiService {

  constructor(
    public http: HttpClient,
     @Inject('BASE_URL') public baseUrl: string) {
  }

  getMovies(): Observable<Movie[]> {

    return this.http.get<Movie[]>(this.baseUrl + 'api/movie')

  }

  findMovies(title): Observable<Movie[]>{
    return this.http.post<Movie[]>(this.baseUrl + 'api/movie',{title: title});
  }

}
