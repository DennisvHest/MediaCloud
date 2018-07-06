import { Component, OnInit } from '@angular/core';
import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { LibraryService } from '../../libraries/library.service';
import { ItemCardModel } from '../../models/view-models/item-card-model';

@Component({
  selector: 'mc-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  libraries: Library<Item>[];
  itemCardModels: Map<Library<Item>, ItemCardModel[]>;

  constructor(private libraryService: LibraryService) { }

  ngOnInit() {
    this.libraryService.getHome()
      .subscribe(libraries => {
        this.libraries = libraries;

        this.itemCardModels = new Map();

        for (const library of this.libraries) {
          this.itemCardModels.set(library, library.media.map(m => new ItemCardModel(null, null, null, m)));
        }
      });
  }

}
