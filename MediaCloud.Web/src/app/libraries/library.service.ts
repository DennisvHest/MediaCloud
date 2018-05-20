import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Library } from '../models/library';
import { Item } from '../models/item';

@Injectable()
export class LibraryService {

  private static librariesUrl = 'api/libraries';

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(LibraryService.librariesUrl);
  }

  get(id: number): Observable<Library<Item>> {
    return this.http.get<Library<Item>>(`${LibraryService.librariesUrl}/${id}`);
  }
}
