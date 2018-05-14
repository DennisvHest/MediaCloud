import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { MaterializeModule } from 'angular2-materialize';
import { SideNavComponent } from './side-nav/side-nav.component';
import { LibraryService } from './libraries/library.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LibraryDetailComponent } from './libraries/library-detail/library-detail.component';
import { LibrariesModule } from './libraries/libraries.module';
import { ItemCardComponent } from './items/item-card/item-card.component';
import { PlayerComponent } from './media/player/player.component';
import { MediaModule } from './media/media.module';

const appRoutes: Routes = [
  { path: '', component: AppComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    SideNavComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    MaterializeModule,
    RouterModule.forRoot(
      appRoutes,
    ),
    LibrariesModule,
    MediaModule
  ],
  providers: [
    LibraryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
