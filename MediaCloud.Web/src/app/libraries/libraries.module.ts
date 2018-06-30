import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LibraryDetailComponent } from './library-detail/library-detail.component';
import { LibraryService } from './library.service';
import { LibraryRoutingModule } from './libraries-routing.module';
import { LayoutModule } from '../layout/layout.module';
import { ItemsModule } from '../items/items.module';
import { McCommonModule } from '../common/mc-common.module';

@NgModule({
    imports: [
      CommonModule,
      LibraryRoutingModule,
      LayoutModule,
      ItemsModule,
      McCommonModule
    ],
    declarations: [
      LibraryDetailComponent
    ],
    providers: [ LibraryService ],
    exports: [
      LibraryDetailComponent
    ]
  })
  export class LibrariesModule {}
