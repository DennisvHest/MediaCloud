import { Component, OnInit } from '@angular/core';
import { Season } from '../../models/season';
import { ItemService } from '../item.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { AppSettings } from '../../../AppSettings';

@Component({
  selector: 'season-detail',
  templateUrl: './season-detail.component.html',
  styleUrls: ['./season-detail.component.css']
})
export class SeasonDetailComponent implements OnInit {

  season: Season;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.itemService.getSeason(+params.get('id')))
      .subscribe(r => {
        this.season = r
      });
  }

  get episodeCount(): number {
    return this.season.episodes.length;
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.season.posterPath != null ? this.season.posterPath : this.season.series.posterPath, "w342");
  }
}
