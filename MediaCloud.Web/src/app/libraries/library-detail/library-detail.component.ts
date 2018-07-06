import { Component, OnInit } from '@angular/core';
import { LibraryService } from '../library.service';
import { ActivatedRoute, ParamMap } from '@angular/router';


import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { ItemCardModel } from '../../models/view-models/item-card-model';

@Component({
  selector: 'mc-library-detail',
  templateUrl: './library-detail.component.html',
  styleUrls: ['./library-detail.component.css']
})
export class LibraryDetailComponent implements OnInit {

  public library: Library<Item>;
  public itemCardmodels: ItemCardModel[];

  constructor(
    private route: ActivatedRoute,
    private libraryService: LibraryService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.libraryService.getItems(+params.get('id')))
      .subscribe(r => {
        this.library = r;
        this.itemCardmodels = r.items.map(i => new ItemCardModel(i));
      });
  }

}
