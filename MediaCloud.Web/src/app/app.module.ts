import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { LibraryService } from './libraries/library.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LibraryDetailComponent } from './libraries/library-detail/library-detail.component';
import { LibrariesModule } from './libraries/libraries.module';
import { ItemCardComponent } from './items/item-card/item-card.component';
import { PlayerComponent } from './media/player/player.component';
import { MediaModule } from './media/media.module';
import { LibraryRoutingModule } from './libraries/libraries-routing.module';
import { SideNavComponent } from './layout/side-nav/side-nav.component';
import { SideNavService } from './layout/side-nav/side-nav.service';
import { LayoutModule } from './layout/layout.module';
import { ItemRoutingModule } from './items/items-routing.module';
import { ItemsModule } from './items/items.module';

const appRoutes: Routes = [
  { path: '', component: AppComponent }
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
    LibraryRoutingModule,
    ItemRoutingModule,
    LayoutModule,
    LibrariesModule,
    ItemsModule,
    MediaModule
  ],
  providers: [
    LibraryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
