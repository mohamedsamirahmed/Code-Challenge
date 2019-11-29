import { environment } from "../../../environments/environment.prod";
import { HttpClient } from "@angular/common/http";
import { CustomerVehicle } from "../../models/customer-vehicle";
import { Injectable } from "@angular/core";

@Injectable()
export class CustomerVehicleDashboardService {

  vehicleServiceBaseUrl = environment.VehicleServiceApiEndpoint;
  VehicleHistoryBaseUrl = environment.VehicleHistoryApiEndpoint;
  constructor(private http: HttpClient) { }

  getcustomerVehiclesList() {
    return this.http.get<CustomerVehicle[]>(this.vehicleServiceBaseUrl + 'CustomerVehicles');
  }

  getcustomerVehicleDetails(customerId, vehicleId, regNo) {
    return this.http.get<CustomerVehicle>(this.VehicleHistoryBaseUrl + 'CustomerVehicleHistory/' + vehicleId + '/' + customerId + '/' + regNo);
  }
}
