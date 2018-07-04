import { Component, OnInit } from '@angular/core';
import { Library } from '../../models/library';
import { Item } from '../../models/item';
import { LibraryService } from '../../libraries/library.service';

@Component({
  selector: 'mc-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  libraries: Library<Item>[];

  constructor(private libraryService: LibraryService) { }

  ngOnInit() {
    this.libraryService.getHome()
      .subscribe(libraries => {
        this.libraries = libraries;
      });
  }

}
