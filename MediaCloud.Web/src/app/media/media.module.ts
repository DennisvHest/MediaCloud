import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MediaRoutingModule } from './media-routing.module';
import { PlayerComponent } from './player/player.component';
import { MediaCardComponent } from './media-card/media-card.component';
import { McCommonModule } from '../common/mc-common.module';

@NgModule({
    imports: [
        CommonModule,
        MediaRoutingModule,
        McCommonModule
    ],
    declarations: [
        PlayerComponent,
        MediaCardComponent
    ],
    providers: [],
    exports: [
        MediaCardComponent
    ]
})
export class MediaModule { }
