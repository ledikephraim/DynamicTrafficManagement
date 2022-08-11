import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
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

  @ViewChild('dialogRef')
  dialogRef!: TemplateRef<any>;

  appTitle = environment.applicationName;
  loading = false;
  userForm!: FormGroup;

  constructor(
    private logger: NGXLogger,
    private notificationService: NotificationService,
    private titleService: Title,
    private regionService: RegionService,
    public dialog: MatDialog

  ) { }

  ngOnInit() {
    this.titleService.setTitle(`${this.appTitle} - Customers`);

    this.userForm = new FormGroup({
      'id': new FormControl(null),
      'name': new FormControl(null, Validators.required),
    });

    this.regionService.getRegions().subscribe(regions => {
      this.regions = regions;
      this.dataSource.data = this.regions;
      this.dataSource.sort = this.sort;

      this.logger.log('Customers loaded');
      this.notificationService.openSnackBar('Regions loaded');
    });
  }
  getRecord(row: any) {
    console.log('the row ', row);
  }
  async save() {
    this.userForm.value.id = 0;
    this.userForm.value.intersections = [];
    this.loading = true;
    this.regionService.saveRegion(this.userForm.value).subscribe(
      response => {
        this.userForm.reset();
        this.regionService.getRegions().subscribe(regions => {
          this.dataSource.data = regions;
          this.loading = false;
          this.dialog.closeAll();

        });

      }
    );

  }

  openTempDialog() {
    const myTempDialog = this.dialog.open(this.dialogRef);
    myTempDialog.afterClosed().subscribe((res) => {


    });
  }
  onClose() {
    this.dialog.closeAll();
  }

}
