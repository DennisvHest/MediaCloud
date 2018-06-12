import { Directive, ElementRef, Input, HostListener } from '@angular/core';

@Directive({
  selector: '[mcImagePlaceholder]'
})
export class ImagePlaceholderDirective {

  @Input() hiResImageUrl: string;

  constructor(private element: ElementRef) {
    this.element.nativeElement.style.opacity = '0.5';
    this.element.nativeElement.style.transition = 'opacity 0.5s ease';
  }

  @HostListener('load') onMouseLeave() {
    this.element.nativeElement.setAttribute('src', this.hiResImageUrl);
    this.element.nativeElement.style.opacity = '1';
  }

}
