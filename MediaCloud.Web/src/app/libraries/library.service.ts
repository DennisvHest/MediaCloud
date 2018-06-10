import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Library } from '../models/library';
import { Item } from '../models/item';
import { HttpClient } from '@angular/common/http';

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
