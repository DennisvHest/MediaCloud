import { Component, OnInit, Input } from '@angular/core';
import { Media } from '../../models/media';
import { AppSettings } from '../../../AppSettings';

@Component({
  selector: 'mc-media-card',
  templateUrl: './media-card.component.html',
  styleUrls: ['./media-card.component.css']
})
export class MediaCardComponent implements OnInit {

  @Input() media: Media;

  mediaTitle: string;
  itemLink: string[];

  itemType: string;

  constructor() { }

  ngOnInit() {
    if (this.media.movie != null) {
      this.itemType = 'Movie';
      this.mediaTitle = this.media.movie.title;
      this.itemLink = ['/items', this.media.movie.id.toString()];
    } else {
      this.itemType = 'Series';
      this.mediaTitle = this.media.episode.season.series.title;
      this.itemLink = ['/items', this.media.episode.season.series.id.toString()];
    }
  }

  get posterUrl(): string {
    if (this.media.movie != null) {
      return AppSettings.imageUrl(this.media.movie.posterPath, 'w342');
    } else {
      return AppSettings.imageUrl(this.media.episode.season.series.posterPath, 'w342');
    }
  }

  get posterPlaceHolder(): string {
    return AppSettings.posterPlaceHolder;
  }

}
