import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Media } from '../../models/media';
import { MediaService } from '../meda.service';
import { ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

declare var noUiSlider: any;

@Component({
  selector: 'mc-player',
  templateUrl: './player.component.html',
  styleUrls: [
    './player.component.css'
  ]
})
export class PlayerComponent implements OnInit {

  @ViewChild('mediaPlayer') mediaPlayerInput: ElementRef<HTMLVideoElement>;
  @ViewChild('mediaProgress') mediaProgressInput: ElementRef<HTMLElement>;
  @ViewChild('overlay') overlay: ElementRef<HTMLElement>;
  @ViewChild('playButton') playButton: ElementRef<HTMLElement>;

  media: Media;
  mediaPlayer: HTMLVideoElement;

  progressBar: any;
  progressPercentage: number;

  overlayTimerHandle: number;

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

    this.progressBar = noUiSlider.create(this.mediaProgressInput.nativeElement, {
      start: 0,
      range: {
        'min': 0,
        'max': 100
      },
      connect: [true, false],
      animate: true,
      animationDuration: 100
    });

    // If mouse is moving on screen, show the overlay. Hide it after 2 seconds of not moving.
    document.addEventListener('mousemove', () => {
      if (this.overlay) {
        this.overlay.nativeElement.classList.remove('hidden');
        document.body.style.cursor = 'initial';

        if (this.overlayTimerHandle) {
          window.clearTimeout(this.overlayTimerHandle);
        }

        this.overlayTimerHandle = window.setTimeout(() => {
          this.overlay.nativeElement.classList.add('hidden');
          document.body.style.cursor = 'none';
        }, 2000);
      }
    });
  }

  onLoadedMetadata() {
    this.mediaPlayer = this.mediaPlayerInput.nativeElement;
  }

  playPause() {
    if (this.mediaPlayer.paused) {
      this.mediaPlayer.play();
      this.playButton.nativeElement.innerHTML = '<i class="material-icons">pause</i>';
    } else {
      this.mediaPlayer.pause();
      this.playButton.nativeElement.innerHTML = '<i class="material-icons">play_arrow</i>';
    }
  }

  updateProgress() {
    // Calculate progress and change the progress on the progress bar.
    this.progressPercentage = (100 / this.mediaPlayer.duration) * this.mediaPlayer.currentTime;
    this.progressBar.set(this.progressPercentage);
  }
}
