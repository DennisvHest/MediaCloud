import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemDetailComponent } from './item-detail/item-detail.component';
import { ItemRoutingModule } from './items-routing.module';
import { LayoutModule } from '../layout/layout.module';
import { MaterializeModule } from 'angular2-materialize';
import { ItemService } from './item.service';

@NgModule({
  imports: [
    CommonModule,
    ItemRoutingModule,
    LayoutModule,
    MaterializeModule
  ],
  declarations: [ItemDetailComponent],
  providers: [ItemService],
  exports: [ItemDetailComponent]
})
export class ItemsModule { }
