import { Component, OnInit, Input } from '@angular/core';
import { BookModel, BooksRequestService } from '../../services/books-request.service';
import { Permission, LoginService } from '../../services/login.service';
import { Observable, BehaviorSubject } from 'rxjs';
import {mergeMap, tap} from 'rxjs/operators';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  @Input()
  permissions?: Permission[];
  hasPermissions = true;

  books$ = new BehaviorSubject<BookModel[]>([]);

  constructor(private service: BooksRequestService,
              private authService: LoginService) {
      console.log('constructing');
    }

  ngOnInit(): void {
    this.checkPermissions();
  }

  loadData(): void {
    this.service.getAll()
    .subscribe(books => this.books$.next(books));
  }

  addBook(model: BookModel): void {
    this.service.add(model)
      .subscribe(addedBook =>
        this.books$.next([...this.books$.value, addedBook]));
  }

  updateBook(model: BookModel): void {
    this.service.update(model)
      .subscribe(() =>
        this.books$.next(
          this.books$.value.map(b => b.id === model.id
            ? model
            : b))
      );
  }
  removeBook(id: number): void {
    this.service.remove(id)
      .subscribe(() =>
        this.books$.next(
          this.books$.value.filter(b => b.id !== id)));
  }

  checkPermissions(): void {
    const requiredPermissions = this.permissions;

    this.authService.token$
      .subscribe((token) => {
        if (!requiredPermissions) { return; }
        for (const permission of requiredPermissions) {
          const hasPermission = token.permissions.includes(permission);

          if (!hasPermission) {
            this.hasPermissions = false;
            return;
          }
        }
        this.hasPermissions = true;
      });
  }
}
