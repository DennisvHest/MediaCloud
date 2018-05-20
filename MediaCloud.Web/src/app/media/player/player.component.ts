import { Component, OnInit, EventEmitter } from '@angular/core';
import { MaterializeAction } from 'angular2-materialize';
import { SideNavService } from '../../layout/side-nav/side-nav.service';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  constructor(private sideNavService: SideNavService) { }

  ngOnInit() {
    this.sideNavService.close();
  }

  onLoad() {
    let mediaPlayer: HTMLVideoElement = <HTMLVideoElement>document.getElementById("media-player");
    mediaPlayer.currentTime = 5;
  }
}
