import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemDetailComponent } from './item-detail/item-detail.component';
import { ItemRoutingModule } from './items-routing.module';
import { LayoutModule } from '../layout/layout.module';
import { MaterializeModule } from 'angular2-materialize';
import { ItemCardComponent } from './item-card/item-card.component';
import { SeasonCardComponent } from './season-card/season-card.component';
import { SeasonDetailComponent } from './season-detail/season-detail.component';
import { EpisodeCardComponent } from './episode-card/episode-card.component';
import { EpisodeDetailComponent } from './episode-detail/episode-detail.component';
import { McCommonModule } from '../common/mc-common.module';

@NgModule({
  imports: [
    CommonModule,
    ItemRoutingModule,
    LayoutModule,
    MaterializeModule,
    McCommonModule
  ],
  declarations: [
    ItemDetailComponent,
    ItemCardComponent,
    SeasonCardComponent,
    SeasonDetailComponent,
    EpisodeCardComponent, EpisodeDetailComponent
  ],
  exports: [
    ItemDetailComponent,
    ItemCardComponent,
    SeasonCardComponent,
    EpisodeCardComponent
  ]
})
export class ItemsModule { }
