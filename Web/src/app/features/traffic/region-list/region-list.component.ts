import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Title } from '@angular/platform-browser';
import { NGXLogger } from 'ngx-logger';
import { NotificationService } from 'src/app/core/services/notification.service';
import { RegionService } from 'src/app/core/services/region.service';
import { environment } from 'src/environments/environment';

export interface Region {
  id: number;
  name: string;
}

@Component({
  selector: 'app-region-list',
  templateUrl: './region-list.component.html',
  styleUrls: ['./region-list.component.css']
})
export class RegionListComponent implements OnInit {
  regions!: Region[];
  displayedColumns: string[] = ['id', 'name'];
  dataSource = new MatTableDataSource(this.regions);

  @ViewChild(MatSort, { static: true })
  sort: MatSort = new MatSort;
  appTitle = environment.applicationName;
  constructor(
    private logger: NGXLogger,
    private notificationService: NotificationService,
    private titleService: Title,
    private regionService : RegionService
  ) { }

  ngOnInit() {
    this.titleService.setTitle(`${this.appTitle} - Customers`);
    this.regionService.getRegions().subscribe(regions=>{
      this.regions = regions;
      this.dataSource.data = this.regions;
      this.dataSource.sort = this.sort;

      this.logger.log('Customers loaded');
      this.notificationService.openSnackBar('Regions loaded');
    });
  }
  getRecord(row:any){
    console.log('the row ', row);
  }

}
