import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LibraryDetailComponent } from "./library-detail/library-detail.component";
import { LibraryService } from "./library.service";
import { LibraryRoutingModule } from "./libraries-routing.module";
import { ItemCardComponent } from "../items/item-card/item-card.component";
import { AppModule } from "../app.module";
import { LayoutModule } from "../layout/layout.module";

@NgModule({
    imports: [
      CommonModule,
      LibraryRoutingModule,
      LayoutModule
    ],
    declarations: [
      LibraryDetailComponent,
      ItemCardComponent
    ],
    providers: [ LibraryService ],
    exports: [
      LibraryDetailComponent,
      ItemCardComponent
    ]
  })
  export class LibrariesModule {}