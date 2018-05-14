import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  constructor() { }

  ngOnInit() {

  }

  onLoad() {
    debugger;
    let mediaPlayer: HTMLVideoElement = <HTMLVideoElement>document.getElementById("media-player");
    mediaPlayer.currentTime = 5;
  }
}
