import { Component, OnInit } from '@angular/core';
import { Season } from '../../models/season';
import { ItemService } from '../item.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { AppSettings } from '../../../AppSettings';
import 'rxjs/add/operator/switchMap';
import { ItemCardModel } from '../../models/view-models/item-card-model';

@Component({
  selector: 'mc-season-detail',
  templateUrl: './season-detail.component.html',
  styleUrls: ['./season-detail.component.css']
})
export class SeasonDetailComponent implements OnInit {

  season: Season;
  episodeCardModels: ItemCardModel[];

  constructor(
    private route: ActivatedRoute,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.itemService.getSeason(+params.get('id')))
      .subscribe(r => {
        this.season = r;

        for (const episode of this.season.episodes) {
          episode.season = this.season;
        }

        this.episodeCardModels = r.episodes.map(e => new ItemCardModel(null, null, e, null));
      });
  }

  get episodeCount(): number {
    return this.season.episodes.length;
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.season.posterPath != null ? this.season.posterPath : this.season.series.posterPath, 'w342');
  }
}
