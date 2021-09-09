import {Component, Input, OnInit} from '@angular/core';
import {BookModel, BooksDataRequestService} from '../../services/books-data-request.service';
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
  isSendingData = false;

  constructor(private readonly service: BooksDataRequestService) { }

  ngOnInit(): void {
  }

  submit(form: NgForm): void {
    const newBook = form.value as BookModel;

    this.isSendingData = true;
    this.service.add(newBook).subscribe(() => {
      this.isSendingData = false;
    });
  }
}
