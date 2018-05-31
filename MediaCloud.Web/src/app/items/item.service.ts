import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../models/item';
import { Observable } from 'rxjs/Observable';
import { AutocompleteItem } from '../models/autocompleteItem';
import { Season } from '../models/season';

@Injectable()
export class ItemService {

  private static itemsUrl = 'api/items';
  private static seasonsUrl = 'api/seasons';

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Item> {
    return this.http.get<Item>(`${ItemService.itemsUrl}/${id}`);
  }

  getSeason(id: number): Observable<Season> {
    return this.http.get<Season>(`${ItemService.seasonsUrl}/${id}`);
  }

  autocomplete(query: string): Observable<AutocompleteItem[]> {
    return this.http.get<AutocompleteItem[]>(`${ItemService.itemsUrl}/autocomplete/${query}`)
  }
}
