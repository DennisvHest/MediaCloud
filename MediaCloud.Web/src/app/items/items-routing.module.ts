import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { ItemDetailComponent } from "./item-detail/item-detail.component";
import { SeasonDetailComponent } from "./season-detail/season-detail.component";

const itemsRoutes: Routes = [
    { path: ':id', component: ItemDetailComponent },
    { path: 'seasons/:id', component: SeasonDetailComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(itemsRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class ItemRoutingModule { }