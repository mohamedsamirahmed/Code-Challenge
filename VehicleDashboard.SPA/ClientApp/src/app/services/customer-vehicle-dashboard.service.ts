import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../models/Pagination';
import { CustomerVehicle } from '../models/customer-vehicle';
import { Lookup } from '../models/lookup';

@Injectable()

export class CustomerVehicleDashboardService {

  vehicleServiceBaseUrl = environment.VehicleServiceApiEndpoint;
  VehicleHistoryBaseUrl = environment.VehicleHistoryApiEndpoint;
  constructor(private http: HttpClient) { }

  //Get customer vehicles based on paging parameter
  getcustomerVehiclesList(page?, itemsPerPage?, customerVehicleParams?): Observable<PaginatedResult<CustomerVehicle[]>> {

    const paginatedResult: PaginatedResult<CustomerVehicle[]> = new PaginatedResult<CustomerVehicle[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (customerVehicleParams.Customer) {
      params = params.append('customer', customerVehicleParams.Customer);
    }
    if (customerVehicleParams.Vehicle) {
      params = params.append('vehicle', customerVehicleParams.Vehicle);
    }


    return this.http.get<CustomerVehicle[]>(this.vehicleServiceBaseUrl + 'CustomerVehicles', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }
          return paginatedResult;
        })
      )

  }

  //get all lookup values
  getlookupList(serviceEndPoint): Observable<Lookup[]> {
    return this.http.get<Lookup[]>(serviceEndPoint);
  }


  getcustomerVehicleDetails(customerId, vehicleId, regNo, page?, itemsPerPage?): Observable<PaginatedResult<CustomerVehicle[]>> {

    const paginatedResult: PaginatedResult<CustomerVehicle[]> = new PaginatedResult<CustomerVehicle[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<CustomerVehicle[]>(this.VehicleHistoryBaseUrl + 'CustomerVehicleHistory/' + vehicleId + '/' + customerId + '/' + regNo, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }
          return paginatedResult;
        })
      );
  }
}
