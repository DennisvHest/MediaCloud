import { Component, OnInit } from '@angular/core';
import { Library } from '../models/library';
import { Item } from '../models/item';
import { LibraryService } from '../library.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {

  libraries: Library<Item>[];

  constructor(private libraryService: LibraryService) { }

  ngOnInit() {
    this.getAllLibraries();
  }

  getAllLibraries() {
    this.libraryService.getAll()
      .subscribe(libraries => {
        console.log(libraries);
        this.libraries = libraries;
      });
  }

}
