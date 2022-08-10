import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Intersection } from 'src/app/features/traffic/intersection-list/intersection-list.component';
import { Region } from 'src/app/features/traffic/region-list/region-list.component';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IntersectionsService {

  TrafficAPI = environment.trafficManagementAPI;
  constructor(private httpClient: HttpClient) {

  }
  public getIntersections(): Observable<Intersection[]> {
    return this.httpClient.get<Intersection[]>(this.TrafficAPI.concat('/Intersection'));
  }
}
