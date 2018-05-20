import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavComponent } from './side-nav/side-nav.component';
import { SideNavService } from './side-nav/side-nav.service';
import { MaterializeModule } from 'angular2-materialize';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    MaterializeModule,
    RouterModule
  ],
  declarations: [
    SideNavComponent
  ],
  providers: [
    SideNavService
  ],
  exports: [
    SideNavComponent
  ]
})
export class LayoutModule { }
