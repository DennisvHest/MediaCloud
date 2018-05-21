import { Component, OnInit } from '@angular/core';
import { ItemService } from '../item.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Item } from '../../models/item';
import { AppSettings } from '../../../AppSettings';

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
      .subscribe(r => this.item = r);
  }

  get backDropUrl(): string {
    if (this.item !== undefined) {
      return AppSettings.imageUrl(this.item.backdropPath, "original");
    } else {
      return "";
    }
  }
}
