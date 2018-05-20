import { Component, OnInit, EventEmitter } from '@angular/core';
import { MaterializeAction } from 'angular2-materialize';
import { SideNavService } from './side-nav.service';
import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { LibraryService } from '../../libraries/library.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  libraries: Library<Item>[];

  sideNavActions = new EventEmitter<string | MaterializeAction>();
  sideNavParams = [
    {
      
    }
  ];

  isOpen = true;

  constructor(
    private sideNavService: SideNavService,
    private libraryService: LibraryService) { }

  ngOnInit() {
    this.getAllLibraries();

    this.sideNavService.change.subscribe(isOpen => {
      this.isOpen = isOpen;
      
      if (isOpen) {
        this.sideNavActions.emit({action: "sideNav", params: ["show"]});
      } else {
        this.sideNavActions.emit({action: "sideNav", params: ["hide"]});
      }      
    });
  }

  getAllLibraries() {
    this.libraryService.getAll()
      .subscribe(libraries => {
        this.libraries = libraries;
      });
  }

}
