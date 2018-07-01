import { Injectable, OnDestroy } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Library } from '../models/library';
import { Item } from '../models/item';
import { HttpClient } from '@angular/common/http';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Injectable()
export class LibraryService implements OnDestroy {

  private static librariesUrl = 'api/libraries';
  private _hubConnection: HubConnection | undefined;

  libraryUpdated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/library')
      .build();
  }

  getList(): Observable<any> {
    return this.http.get(LibraryService.librariesUrl + '/list');
  }

  getItems(id: number): Observable<Library<Item>> {
    return this.http.get<Library<Item>>(`${LibraryService.librariesUrl}/${id}/items`);
  }

  async create(name: string, type: number, folderPath: string, onProgressReport: (progress: any) => void): Promise<number> {
    // Bind progress report callbacks.
    this._hubConnection.off('progressReport');
    this._hubConnection.on('progressReport', (data: any) => {
      console.log(data);
      onProgressReport(data);
    });

    // Open SignalR connection, send the library create request, then close the connection when finished.
    await this._hubConnection.start();
    const newLibraryId: number = await this._hubConnection.invoke('Create', type, name, folderPath);
    await this._hubConnection.stop();

    // Inform library subscribers that there is a new library
    this.libraryUpdated.next(true);

    return newLibraryId;
  }

  ngOnDestroy(): void {
    this._hubConnection.stop();
  }

  delete(id: number) {
    this.http.delete(`${LibraryService.librariesUrl}/${id}`).subscribe(() => {
      this.libraryUpdated.next(true);
    });
  }
}
