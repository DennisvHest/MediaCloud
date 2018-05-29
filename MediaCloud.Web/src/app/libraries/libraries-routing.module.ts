import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { LibraryDetailComponent } from "./library-detail/library-detail.component";

const librariesRoutes: Routes = [
    { path: ':id', component: LibraryDetailComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(librariesRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class LibraryRoutingModule { }