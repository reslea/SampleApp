import { Component, OnInit, Input } from '@angular/core';
import { BookModel, BooksService } from '../../services/books.service';
import { Permission, LoginService } from '../../services/login.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { mergeMap } from 'rxjs/operators';

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

  constructor(private service: BooksService,
    private authService: LoginService) { 
      console.log('constructing');
    }

  ngOnInit(): void {
    this.checkPermissions();
  }

  loadData() {
    this.service.getAll()
    .subscribe(books => this.books$.next(books));
  }

  checkPermissions() {
    const requiredPermissions = this.permissions;

    this.authService.token$
      .subscribe((token) => {
        if(!requiredPermissions) return;
        for(const permission of requiredPermissions) {
          const hasPermission = token.permissions.includes(permission);

          if(!hasPermission) {
            this.hasPermissions = false;
            return;
          }
        }
        this.hasPermissions = true;
      });
  }
}
