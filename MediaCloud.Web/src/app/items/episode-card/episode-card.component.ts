import { Component, OnInit, Input } from '@angular/core';
import { Episode } from '../../models/episode';
import { AppSettings } from '../../../AppSettings';
import { Item } from '../../models/item';

@Component({
  selector: 'mc-episode-card',
  templateUrl: './episode-card.component.html',
  styleUrls: ['./episode-card.component.css']
})
export class EpisodeCardComponent implements OnInit {

  @Input() episode: Episode;
  @Input() parentSeries: Item;

  constructor() { }

  ngOnInit() {
  }

  get stillUrl(): string {
    return AppSettings.imageUrl(this.episode.stillPath !== undefined ? this.episode.stillPath : this.parentSeries.backdropPath, 'w300');
  }
}
