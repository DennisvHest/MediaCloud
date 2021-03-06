import { Component, OnInit } from '@angular/core';
import { Select2OptionData } from 'ng2-select2';
import { ItemService } from '../../items/item.service';
import { Router } from '@angular/router';
import { LayoutService } from '../layout.service';

@Component({
  selector: 'mc-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent implements OnInit {

  autocompleteData: Select2OptionData[];
  autocompleteOptions: Select2Options;

  constructor(
    private layoutService: LayoutService,
    private router: Router
  ) { }

  ngOnInit() {
    // Initialize the select2 autocomplete box
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
      placeholder: 'Search for items in your libraries...'
    };
  }

  onMenuClick() {
    this.layoutService.sideNavIsOpen.next(!this.layoutService.sideNavIsOpen.getValue());
  }

  onAutocompleteSelect(input: any) {
    // Navigate to the detail
    const value: string = input.value[0];
    const valueSplit = value.split('_');
    const itemId = valueSplit[0];
    const episodeId = valueSplit[1];

    // If there is an episode, navigate to the episode. Otherwise, navigate to the item.
    if (episodeId) {
      this.router.navigate(['items', 'episodes', episodeId]);
    } else {
      this.router.navigate(['items', itemId]);
    }

    // Remove selection from select box
    const element = input.data[0].element.remove();
  }
}
