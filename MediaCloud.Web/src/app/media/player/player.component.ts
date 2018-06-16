import { Component, OnInit, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { Media } from '../../models/media';
import { MediaService } from '../meda.service';
import { ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'mc-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']

})
export class PlayerComponent implements OnInit {

  @ViewChild('mediaPlayer') mediaPlayerInput: ElementRef;

  media: Media;
  mediaPlayer: HTMLVideoElement;
  progressPercentage: number;

  constructor(
    private route: ActivatedRoute,
    private mediaService: MediaService
  ) { }

  ngOnInit() {
    this.route.paramMap.switchMap((params: ParamMap) =>
      this.mediaService.get(+params.get('id')))
      .subscribe(r => {
        this.media = r;
      });
  }

  onLoadedMetadata() {
    this.mediaPlayer = this.mediaPlayerInput.nativeElement;
  }

  playPause() {
    if (this.mediaPlayer.paused) {
      this.mediaPlayer.play();
    } else {
      this.mediaPlayer.pause();
    }
  }

  updateProgress() {
    this.progressPercentage = (100 / this.mediaPlayer.duration) * this.mediaPlayer.currentTime;
  }
}
