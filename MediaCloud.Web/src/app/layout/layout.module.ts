import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavComponent } from './side-nav/side-nav.component';
import { MaterializeModule } from 'angular2-materialize';
import { RouterModule } from '@angular/router';
import { TopNavComponent } from './top-nav/top-nav.component';
import { LayoutComponent } from './layout/layout.component';
import { Select2Module } from 'ng2-select2';
import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';

@NgModule({
  imports: [
    CommonModule,
    MaterializeModule,
    RouterModule,
    Select2Module
  ],
  declarations: [
    SideNavComponent,
    TopNavComponent,
    LayoutComponent
  ],
  exports: [
    SideNavComponent
  ]
})
export class LayoutModule { }
