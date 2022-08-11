import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TrafficRoutingModule } from './traffic-routing.module';
import { RegionListComponent } from './region-list/region-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { IntersectionListComponent } from './intersection-list/intersection-list.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    RegionListComponent,
    IntersectionListComponent
  ],
  imports: [
    CommonModule,
    TrafficRoutingModule,
    SharedModule,
    MatDialogModule,

  ]
})
export class TrafficModule { }
