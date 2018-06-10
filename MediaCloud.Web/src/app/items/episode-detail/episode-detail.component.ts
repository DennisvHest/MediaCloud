import { Component, OnInit } from '@angular/core';
import { Episode } from '../../models/episode';
import { AppSettings } from '../../../AppSettings';
import { ItemService } from '../item.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';

@Component({
  selector: 'mc-episode-detail',
  templateUrl: './episode-detail.component.html',
  styleUrls: ['./episode-detail.component.css']
})
export class EpisodeDetailComponent implements OnInit {

  episode: Episode;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.itemService.getEpisode(+params.get('id')))
      .subscribe(r => {
        this.episode = r;
      });
  }

  get stillUrl(): string {
    return AppSettings.imageUrl(
      this.episode.stillPath !== undefined
      ? this.episode.stillPath
      : this.episode.season.series.backdropPath, 'w300'
    );
  }
}
