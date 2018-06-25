import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Directory } from '../models/directory';

@Injectable()
export class DirectoryService {

  private static librariesUrl = 'api/directories';

  constructor(private http: HttpClient) { }

  getDrives(): Observable<Directory[]> {
    return this.http.get<Directory[]>(`${DirectoryService.librariesUrl}/drives`);
  }

  getSubDirectories(path: string): Observable<Directory[]> {
    return this.http.get<Directory[]>(`${DirectoryService.librariesUrl}?path=${path}`);
  }
}
