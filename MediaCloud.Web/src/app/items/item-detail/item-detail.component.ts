import { Component, OnInit } from '@angular/core';
import { ItemService } from '../item.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Item } from '../../models/item';
import { AppSettings } from '../../../AppSettings';
import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {

  item: Item;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemService: ItemService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.itemService.get(+params.get('id')))
      .subscribe(r => {
        r.releaseDate = new Date(r.releaseDate);
        this.item = r
      });
  }

  get backDropUrl(): string {
    return AppSettings.imageUrl(this.item.backdropPath, "w300");
  }

  get posterUrl(): string {
    return AppSettings.imageUrl(this.item.posterPath, "w342");
  }

  get episodeCount(): number {
    return this.item.seasons
      .map(s => s.episodes)
      .reduce((e1, e2) => e1.concat(e2)).length;
  }
}
