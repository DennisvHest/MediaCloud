import { Component, OnInit, EventEmitter } from '@angular/core';
import { MaterializeAction } from 'angular2-materialize';
import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { LibraryService } from '../../libraries/library.service';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'mc-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  libraries: Library<Item>[];

  sideNavActions = new EventEmitter<string | MaterializeAction>();

  addLibraryModalParams = [];
  addLibraryModalActions = new EventEmitter<string | MaterializeAction>();
  loadLibraryAddModal = false;

  isOpen = true;

  constructor(
    private libraryService: LibraryService,
    private router: Router
  ) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.addLibraryModalActions.emit({ action: 'modal', params: ['close'] });
      }
        this.loadLibraryAddModal = false;
    });
  }

  ngOnInit() {
    this.libraryService.libraryUpdated.subscribe((libraryUpdated) => {
      if (libraryUpdated) {
        this.getAllLibraries();
      }
    });
    this.getAllLibraries();
  }

  getAllLibraries() {
    this.libraryService.getList()
      .subscribe(libraries => {
        this.libraries = libraries;
      });
  }

  openAddLibraryModal() {
    this.loadLibraryAddModal = true;
    this.addLibraryModalActions.emit({ action: 'modal', params: ['open'] });
  }

  faClass(libraryId: number): string {
    const foundLibrary = this.libraries.find(l => l.id === libraryId);

    switch (foundLibrary.libraryType) {
      case 'MovieLibrary':
        return 'film';
      case 'SeriesLibrary':
        return 'tv';
      default:
        return 'folder';
    }
  }
}
