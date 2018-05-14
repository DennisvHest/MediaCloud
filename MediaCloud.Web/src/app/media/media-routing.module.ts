import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { PlayerComponent } from "./player/player.component";

const mediaRoutes: Routes = [
    { path: 'media/:id/play', component: PlayerComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(mediaRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class MediaRoutingModule { }