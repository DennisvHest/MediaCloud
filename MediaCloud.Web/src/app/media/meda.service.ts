import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Media } from '../models/media';

@Injectable({
  providedIn: 'root'
})
export class MediaService {

  private static mediaUrl = 'api/media';

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Media> {
    return this.http.get<Media>(`${MediaService.mediaUrl}/${id}`);
  }
}
