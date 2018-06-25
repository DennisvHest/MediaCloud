import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Library } from '../models/library';
import { Item } from '../models/item';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class LibraryService {

  private static librariesUrl = 'api/libraries';

  libraryUpdated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) { }

  getList(): Observable<any> {
    return this.http.get(LibraryService.librariesUrl + '/list');
  }

  getItems(id: number): Observable<Library<Item>> {
    return this.http.get<Library<Item>>(`${LibraryService.librariesUrl}/${id}/items`);
  }

  create(name: string, type: number, folderPath: string): Observable<any> {
    const body = new FormData();
    body.append('name', name);
    body.append('type', type.toString());
    body.append('folderPath', folderPath);

    return this.http.post(LibraryService.librariesUrl, body);
  }
}
