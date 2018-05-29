import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Select2OptionData } from 'ng2-select2';
import { ItemService } from '../../items/item.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent implements OnInit {

  autocompleteData: Select2OptionData[];
  autocompleteOptions: Select2Options;

  constructor(
    private itemService: ItemService,
    private router: Router
  ) { }

  ngOnInit() {
    //Initialize the select2 autocomplete box
    this.autocompleteOptions = {
      multiple: true,
      maximumSelectionLength: 1,
      minimumInputLength: 1,
      width: '320px',
      ajax: {
        url: 'api/items/autocomplete',
        data: function (params) {
          return {
            query: params.term
          };
        },
        processResults: function (data) {
          return {
            results: data
          };
        },
        delay: 250
      },
    }
  }

  onAutocompleteSelect(input: any) {
    //Navigate to item detail page
    let itemId: number = input.value[0];
    this.router.navigate(['items', itemId]);

    //Remove selection from select box
    let element = input.data[0].element.remove();
  }
}
