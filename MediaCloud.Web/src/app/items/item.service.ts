import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../models/item';
import { Observable } from 'rxjs/Observable';
import { AutocompleteItem } from '../models/autocompleteItem';

@Injectable()
export class ItemService {

  private static itemsUrl = 'api/items';

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Item> {
    return this.http.get<Item>(`${ItemService.itemsUrl}/${id}`);
  }

  autocomplete(query: string): Observable<AutocompleteItem[]> {
    return this.http.get<AutocompleteItem[]>(`${ItemService.itemsUrl}/autocomplete/${query}`)
  }
}
