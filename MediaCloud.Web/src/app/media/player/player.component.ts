import { Component, OnInit, EventEmitter } from '@angular/core';
import { MaterializeAction } from 'angular2-materialize';

@Component({
  selector: 'mc-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  constructor() { }

  ngOnInit() {}

  onLoad() {
    const mediaPlayer: HTMLVideoElement = <HTMLVideoElement>document.getElementById('media-player');
    mediaPlayer.currentTime = 5;
  }
}
