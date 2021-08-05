import {Component, Input, OnInit} from '@angular/core';
import {BookModel, BooksRequestService} from '../../services/books-request.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  book: BookModel = {
    title: '',
    author: '',
    pagesCount: 0,
    publishDate: null
  };

  @Input()
  addBook: (BookModel) => void;

  constructor(private readonly service: BooksRequestService) { }

  ngOnInit(): void {
  }

  submit(form: NgForm): void {
    this.addBook(form.value as BookModel);
  }
}
