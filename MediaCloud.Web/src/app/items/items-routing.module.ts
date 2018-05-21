import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { ItemDetailComponent } from "./item-detail/item-detail.component";

const itemsRoutes: Routes = [
    { path: 'items/:id', component: ItemDetailComponent },
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