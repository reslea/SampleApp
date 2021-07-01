import { Component, OnInit } from '@angular/core';
import { BookModel, BooksService } from '../../services/books.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  books: BookModel[] = [];

  constructor(private service: BooksService) { }

  ngOnInit(): void {
  }

  loadData() {
    this.service.getAll().subscribe(books => this.books = books);
  }
}
