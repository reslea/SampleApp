import { Component, OnInit } from '@angular/core';
import { Permission } from '../../services/login.service';

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrls: ['./books-page.component.scss']
})
export class BooksPageComponent implements OnInit {
  public permission = Permission;

  constructor() { }

  ngOnInit(): void {
  }

}
