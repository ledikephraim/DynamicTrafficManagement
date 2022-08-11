import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Region } from 'src/app/features/traffic/region-list/region-list.component';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  TrafficAPI = environment.trafficManagementAPI;
  constructor(private httpClient: HttpClient) {

  }
  public getRegions(): Observable<Region[]> {
    return this.httpClient.get<Region[]>(this.TrafficAPI.concat('/Region'));
  }
  public saveRegion(region: Region): Observable<HttpResponse<Region>> {
    return this.httpClient.post<Region>(this.TrafficAPI.concat('/Region'), region, { observe: 'response' });
  }
}
