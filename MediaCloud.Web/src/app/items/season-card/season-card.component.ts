import { Component, OnInit, Input } from '@angular/core';
import { Season } from '../../models/season';
import { AppSettings } from '../../../AppSettings';
import { Item } from '../../models/item';

@Component({
  selector: 'app-season-card',
  templateUrl: './season-card.component.html',
  styleUrls: ['./season-card.component.css']
})
export class SeasonCardComponent implements OnInit {

  @Input() season: Season;
  @Input() parentItem: Item;

  constructor() { }

  ngOnInit() {
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.season.posterPath != null ? this.season.posterPath : this.parentItem.posterPath, "w342");
  }
}
