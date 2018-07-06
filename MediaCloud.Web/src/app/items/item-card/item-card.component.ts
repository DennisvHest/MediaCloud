import { Component, OnInit, Input } from '@angular/core';
import { AppSettings } from '../../../AppSettings';
import { ItemCardModel } from '../../models/view-models/item-card-model';

@Component({
  selector: 'mc-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {

  @Input() item: ItemCardModel;

  constructor() { }

  ngOnInit() {
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.item.posterPath ? this.item.posterPath : this.item.backupPosterPath, this.item.imageWidth);
  }

  get posterPlaceHolder(): string {
    return AppSettings.posterPlaceHolder;
  }

  get stillUrl(): string {
    return AppSettings.imageUrl(this.item.stillPath ? this.item.stillPath : this.item.backupStillPath, 'w300');
  }

  get stillPlaceHolder(): string {
    return AppSettings.stillPlaceHolder;
  }
}
