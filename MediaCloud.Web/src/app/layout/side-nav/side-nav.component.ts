import { Component, OnInit, EventEmitter } from '@angular/core';
import { MaterializeAction } from 'angular2-materialize';
import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { LibraryService } from '../../libraries/library.service';

@Component({
  selector: 'side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  libraries: Library<Item>[];

  sideNavActions = new EventEmitter<string | MaterializeAction>();

  isOpen = true;

  constructor(private libraryService: LibraryService) { }

  ngOnInit() {
    this.getAllLibraries();
  }

  getAllLibraries() {
    this.libraryService.getAll()
      .subscribe(libraries => {
        this.libraries = libraries;
      });
  }

  faClass(libraryId: number): string {
    let foundLibrary = this.libraries.find(l => l.id == libraryId);

    switch (foundLibrary.libraryType) {
      case "MovieLibrary":
        return "film";
      case "SeriesLibrary":
        return "tv";
      default:
        return "folder";
    }
  }
}
