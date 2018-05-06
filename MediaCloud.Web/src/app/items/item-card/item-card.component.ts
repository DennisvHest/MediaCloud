import { Component, OnInit, Input } from '@angular/core';
import { Item } from '../../models/item';
import { AppSettings } from '../../../AppSettings';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {

  @Input() item: Item;

  constructor() { }

  ngOnInit() {
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.item.posterPath, "w342");
  }

}
