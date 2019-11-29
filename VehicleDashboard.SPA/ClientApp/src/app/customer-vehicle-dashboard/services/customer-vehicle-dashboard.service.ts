import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CustomerVehicle } from '../../models/customer-vehicle';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { PaginatedResult } from '../../models/Pagination';
import { map } from 'rxjs/operators';

@Injectable()

export class CustomerVehicleDashboardService {

  vehicleServiceBaseUrl = environment.VehicleServiceApiEndpoint;
  VehicleHistoryBaseUrl = environment.VehicleHistoryApiEndpoint;
  constructor(private http: HttpClient) { }

  getcustomerVehiclesList(page?, itemsPerPage?): Observable<PaginatedResult<CustomerVehicle[]>> {
    const PaginatedResult: PaginatedResult<CustomerVehicle[]>;//= new PaginatedResult<CustomerVehicle[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<CustomerVehicle[]>(this.vehicleServiceBaseUrl + 'CustomerVehicles', { observe: 'response', params })
      .pipe(
        map(response => {
          PaginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            PaginatedResult.paginations = JSON.parse(response.headers.get('Pagination'))
          }
          return PaginatedResult;
        })
      )

  }

  getcustomerVehicleDetails(customerId, vehicleId, regNo) {
    return this.http.get<CustomerVehicle>(this.VehicleHistoryBaseUrl + 'CustomerVehicleHistory/' + vehicleId + '/' + customerId + '/' + regNo);
  }
}
