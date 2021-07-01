import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from './login.service';

export interface BookModel {
  title: string,
  author: string,
  publishDate: Date,
  pagesCount: number
}

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  booksApiLink = "https://localhost:5001/api/books";

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<BookModel[]>(this.booksApiLink);
  }
}
