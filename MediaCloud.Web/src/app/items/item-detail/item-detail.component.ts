import { Component, OnInit } from '@angular/core';
import { ItemService } from '../item.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Item } from '../../models/item';
import { AppSettings } from '../../../AppSettings';
import { ItemCardModel } from '../../models/view-models/item-card-model';

@Component({
  selector: 'mc-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {

  item: Item;
  seasonCardModels: ItemCardModel[];

  constructor(
    private route: ActivatedRoute,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.itemService.get(+params.get('id')))
      .subscribe(r => {
        r.releaseDate = new Date(r.releaseDate);
        this.item = r;

        for (const season of this.item.seasons) {
          season.series = this.item;
        }

        this.seasonCardModels = this.item.seasons.map(s => new ItemCardModel(null, s, null, null));
      });
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.item.posterPath, 'w342');
  }

  get episodeCount(): number {
    return this.item.seasons
      .map(s => s.episodes)
      .reduce((e1, e2) => e1.concat(e2)).length;
  }
}
