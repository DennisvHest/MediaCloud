import { Component, OnInit, Input } from '@angular/core';
import { Item } from '../../models/item';
import { AppSettings } from '../../../AppSettings';
import { Library } from '../../models/library';

@Component({
  selector: 'mc-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {

  @Input() item: Item;

  constructor() { }

  ngOnInit() {
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.item.posterPath, 'w342');
  }

  get posterPlaceHolder(): string {
    return AppSettings.posterPlaceHolder;
  }
}
