import { Component, OnInit } from '@angular/core';
import { LibraryService } from '../library.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';


import { Library } from '../../models/library';
import { Item } from '../../models/item';

@Component({
  selector: 'mc-library-detail',
  templateUrl: './library-detail.component.html',
  styleUrls: ['./library-detail.component.css']
})
export class LibraryDetailComponent implements OnInit {

  public library: Library<Item>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private libraryService: LibraryService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.libraryService.get(+params.get('id')))
      .subscribe(r => this.library = r);
  }

}
