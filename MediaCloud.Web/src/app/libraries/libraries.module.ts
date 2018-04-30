import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LibraryDetailComponent } from "./library-detail/library-detail.component";
import { LibraryService } from "./library.service";
import { LibraryRoutingModule } from "./libraries-routing.module";

@NgModule({
    imports: [
      CommonModule,
      LibraryRoutingModule
    ],
    declarations: [
      LibraryDetailComponent
    ],
    providers: [ LibraryService ]
  })
  export class LibrariesModule {}