import { TestBed, ComponentFixture, inject, async, fakeAsync, tick, flush } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { async as _async } from "rxjs/scheduler/async";
import { map } from "rxjs/operators";

import { MovieHomeComponent } from "./component";
import { MovieApiService } from "./service";
import { MockMovieApiService, BASE_URL } from "./mock";
import { DebugElement } from "@angular/core";
import { By } from "@angular/platform-browser";


describe("Movie Component: Fetch", () => {

  let component: MovieHomeComponent;
  let fixture: ComponentFixture<MovieHomeComponent>;
  let service: MovieApiService;

  beforeEach(() => {

    TestBed.configureTestingModule({

      declarations: [MovieHomeComponent],
      providers: [
        { provide: 'BASE_URL', useValue: BASE_URL },
        { provide: MovieApiService, useClass: MockMovieApiService },
      ],
      imports: [HttpClientTestingModule],
    }).overrideComponent(
      MovieHomeComponent,
      {
        set:
        {
          providers: [
            { provide: MovieApiService, useClass: MockMovieApiService },
          ]
        }
      }
    )
      .compileComponents();

    fixture = TestBed.createComponent(MovieHomeComponent);

    component = fixture.componentInstance;

    service = TestBed.get(MovieApiService);

  });

  it('Test initializtion api', async(() => {

    const movies$ = component.movies$.pipe(map((m) => m));

    movies$.subscribe((movies) => {
      expect(movies.length).toBe(2);
    });

  }));

  it('Test search api', async(() => {

    component.ngOnInit();

    component.searchedMovies$.subscribe((movies) => {

      expect(movies.length).toBe(1);
      expect(movies[0].title).toBe("test");

    });


    component.search("test");

  }));

  it('Test movies in DOM', async(() => {

    fixture.detectChanges();

    fixture.whenStable().then(() => {
      const compiled = fixture.debugElement.nativeElement;

      //Below query does not work
      /* let el: DebugElement = fixture.debugElement.query(
        By.css("table tr:first-child td")
      ); */

      const table = compiled.querySelectorAll("table tr")

      expect( table.length).toBe(3);
      expect(table[1].innerText.trim()).toBe("movie1		2019")

    });


  }));

});
