import { environment } from "../../../environments/environment.prod";
import { HttpClient } from "@angular/common/http";
import { CustomerVehicle } from "../../models/customer-vehicle";
import { Injectable } from "@angular/core";

@Injectable()
export class CustomerVehicleDashboardService {
  baseUrl = environment.apiEndpoint;
  constructor(private http: HttpClient) { }

  getcustomerVehiclesList() {
   return this.http.get<CustomerVehicle[]>(this.baseUrl + 'CustomerVehicles');
  }

//  getCustomerVehicleDetails(customerId, vehicleId) {
//    return this.http.get<CustomerVehicle>(this.baseUrl + '/Values' + customerId + '/' + vehicleId);
//  }
}
