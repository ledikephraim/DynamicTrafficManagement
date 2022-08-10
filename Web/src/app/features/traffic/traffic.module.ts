import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TrafficRoutingModule } from './traffic-routing.module';
import { RegionListComponent } from './region-list/region-list.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    RegionListComponent
  ],
  imports: [
    CommonModule,
    TrafficRoutingModule,
    SharedModule
  ]
})
export class TrafficModule { }
