import { Component, OnInit, Input } from '@angular/core';
import { BookModel, BooksDataRequestService } from '../../services/books-data-request.service';
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

  isLoading = false;

  books$: BehaviorSubject<BookModel[]>;

  constructor(private service: BooksDataRequestService,
              private authService: LoginService) {
    this.books$ = service.books$;
  }

  ngOnInit(): void {
    this.checkPermissions();
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.service.getAll().subscribe(() => this.isLoading = false);
  }

  addBook(model: BookModel): void {
    this.service.add(model).subscribe();
  }

  updateBook(model: BookModel): void {
    this.service.update(model).subscribe();
  }
  removeBook(id: number): void {
    this.service.remove(id).subscribe();
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
