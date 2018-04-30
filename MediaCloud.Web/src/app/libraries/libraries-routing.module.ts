import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { LibraryDetailComponent } from "./library-detail/library-detail.component";

const librariesRoutes: Routes = [
    { path: 'libraries/:id', component: LibraryDetailComponent },
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