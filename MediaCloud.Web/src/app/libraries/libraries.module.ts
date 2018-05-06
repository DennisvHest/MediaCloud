import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LibraryDetailComponent } from "./library-detail/library-detail.component";
import { LibraryService } from "./library.service";
import { LibraryRoutingModule } from "./libraries-routing.module";
import { ItemCardComponent } from "../items/item-card/item-card.component";

@NgModule({
    imports: [
      CommonModule,
      LibraryRoutingModule
    ],
    declarations: [
      LibraryDetailComponent,
      ItemCardComponent
    ],
    providers: [ LibraryService ]
  })
  export class LibrariesModule {}