import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../models/item';
import { Observable } from 'rxjs';
import { AutocompleteItem } from '../models/autocompleteItem';
import { Season } from '../models/season';
import { Episode } from '../models/episode';

@Injectable()
export class ItemService {

  private static itemsUrl = 'api/items';
  private static seasonsUrl = 'api/seasons';
  private static episodesUrl = 'api/episodes';

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Item> {
    return this.http.get<Item>(`${ItemService.itemsUrl}/${id}`);
  }

  getSeason(id: number): Observable<Season> {
    return this.http.get<Season>(`${ItemService.seasonsUrl}/${id}`);
  }

  getEpisode(id: number): Observable<Episode> {
    return this.http.get<Episode>(`${ItemService.episodesUrl}/${id}`);
  }

  autocomplete(query: string): Observable<AutocompleteItem[]> {
    return this.http.get<AutocompleteItem[]>(`${ItemService.itemsUrl}/autocomplete/${query}`)
  }
}
