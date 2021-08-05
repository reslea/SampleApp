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
export class BooksRequestService {
  apiLink = 'https://localhost:5001/api/books';

  constructor(private http: HttpClient) { }

  getAll(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(this.apiLink);
  }

  add(model: BookModel): Observable<BookModel> {
    return this.http.post<BookModel>(this.apiLink, model);
  }

  update(model: BookModel): Observable<{}> {
    return this.http.put<{}>(`${this.apiLink}/${model.id}`, model);
  }

  remove(id: number): Observable<{}> {
    return this.http.delete<{}>(`${this.apiLink}/${id}`);
  }
}
