import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {BookModel} from './books-request.service';

@Injectable({
  providedIn: 'root'
})
export class BooksDataService {
  books$ = new BehaviorSubject<BookModel[]>([]);

  constructor() {

  }
}
