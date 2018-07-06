import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MediaRoutingModule } from './media-routing.module';
import { PlayerComponent } from './player/player.component';
import { McCommonModule } from '../common/mc-common.module';

@NgModule({
    imports: [
        CommonModule,
        MediaRoutingModule,
        McCommonModule
    ],
    declarations: [
        PlayerComponent
    ],
    providers: []
})
export class MediaModule { }
