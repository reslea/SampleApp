import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import { tap } from 'rxjs/operators';

export interface BookModel {
  id?: number;
  title: string;
  author: string;
  publishDate: Date;
  pagesCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class BooksDataRequestService {
  apiLink = 'https://localhost:5001/api/books';
  books$ = new BehaviorSubject<BookModel[]>([]);

  constructor(private http: HttpClient) { }

  getAll(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(this.apiLink)
      .pipe(
        tap(books => this.books$.next(books))
      );
  }

  add(model: BookModel): Observable<BookModel> {
    return this.http.post<BookModel>(this.apiLink, model)
      .pipe(
        tap(created => this.books$.next(
          [...this.books$.value, created]
        )));
  }

  update(model: BookModel): Observable<{}> {
    const updatedArr = this.books$.value.map(b => b.id === model.id
      ? model
      : b);

    return this.http.put<{}>(`${this.apiLink}/${model.id}`, model)
      .pipe(
        tap(() => this.books$.next(updatedArr))
      );
  }

  remove(id: number): Observable<{}> {
    const updatedArr = this.books$.value.filter(b => b.id !== id);
    return this.http.delete<{}>(`${this.apiLink}/${id}`)
      .pipe(
        tap(() => this.books$.next(updatedArr))
      );
  }
}
