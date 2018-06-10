import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ItemCardComponent } from '../items/item-card/item-card.component';
import { MediaRoutingModule } from './media-routing.module';
import { PlayerComponent } from './player/player.component';

@NgModule({
    imports: [
        CommonModule,
        MediaRoutingModule
    ],
    declarations: [
        PlayerComponent
    ],
    providers: []
})
export class MediaModule { }
