import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

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
