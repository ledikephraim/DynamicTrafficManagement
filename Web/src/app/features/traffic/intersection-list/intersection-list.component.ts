import { Component, OnInit } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Title } from '@angular/platform-browser';
import { NGXLogger } from 'ngx-logger';
import { IntersectionsService } from 'src/app/core/services/intersections.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { RegionService } from 'src/app/core/services/region.service';
import { environment } from 'src/environments/environment';

export interface Intersection {
  id: number;
  longitude: string;
  latitude: string;
}

@Component({
  selector: 'app-intersection-list',
  templateUrl: './intersection-list.component.html',
  styleUrls: ['./intersection-list.component.css']
})
export class IntersectionListComponent implements OnInit {
  intersections!: Intersection[];
  displayedColumns: string[] = ['id', 'longitude', 'latitude'];
  dataSource = new MatTableDataSource(this.intersections);
  sort: MatSort = new MatSort;
  appTitle = environment.applicationName;
  constructor(
    private logger: NGXLogger,
    private notificationService: NotificationService,
    private titleService: Title,
    private intersectionService : IntersectionsService
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle(`${this.appTitle} - Customers`);
    this.intersectionService.getIntersections().subscribe(intersections=>{
      this.intersections = intersections;
      this.dataSource.data = this.intersections;
      this.dataSource.sort = this.sort;

      this.logger.log('Intersections loaded');
      this.notificationService.openSnackBar('Intersections loaded');
    });
  }
  getRecord(row:any){
    console.log('the row ', row);
  }

}
