import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImagePlaceholderDirective } from './image-placeholder.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    ImagePlaceholderDirective
  ],
  exports: [
    ImagePlaceholderDirective
  ]
})
export class McCommonModule { }
