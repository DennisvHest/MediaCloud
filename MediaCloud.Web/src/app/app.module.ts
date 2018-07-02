import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LibraryService } from './libraries/library.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { MediaModule } from './media/media.module';
import { LayoutModule } from './layout/layout.module';
import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { LayoutComponent } from './layout/layout/layout.component';
import { ItemService } from './items/item.service';
import { HomeComponent } from './home/home/home.component';

const appRoutes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent},
      { path: 'libraries', loadChildren: './libraries/libraries.module#LibrariesModule' },
      { path: 'items', loadChildren: './items/items.module#ItemsModule' },
      { path: 'media', loadChildren: './media/media.module#MediaModule' }
    ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
    ),
    NgProgressModule.forRoot({
      spinner: false,
      color: '#a62626',
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
