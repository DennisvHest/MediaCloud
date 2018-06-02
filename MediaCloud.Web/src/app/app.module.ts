import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { LibraryService } from './libraries/library.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { PlayerComponent } from './media/player/player.component';
import { MediaModule } from './media/media.module';
import { SideNavComponent } from './layout/side-nav/side-nav.component';
import { LayoutModule } from './layout/layout.module';
import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { LayoutComponent } from './layout/layout/layout.component';
import { ItemService } from './items/item.service';
import { SeasonDetailComponent } from './items/season-detail/season-detail.component';

const appRoutes: Routes = [
  { 
    path: '', component: LayoutComponent,
    children: [
      { path: 'libraries', loadChildren: './libraries/libraries.module#LibrariesModule' },
      { path: 'items', loadChildren: './items/items.module#ItemsModule' },
      { path: 'media', loadChildren: './media/media.module#MediaModule' }
    ] 
  }
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
    ),
    NgProgressModule.forRoot({
      spinner: false,
      color: '#3f7ad2',
      thick: true
    }),
    NgProgressHttpModule,
    LayoutModule,
    MediaModule
  ],
  providers: [
    LibraryService,
    ItemService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
