import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-vehicleDashboard',
  templateUrl: './vehicleDashboard.component.html',
})
export class VehicleDashboardComponent implements OnInit {
  vehicleCustomers: any;
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.GetCustomerVehicles();
    }

  GetCustomerVehicles() {
    this.http.get(environment.apiEndpoint + 'api/Values/Get').subscribe(response => {
      this.vehicleCustomers = response;
    }, error => {
      console.log(error);
      })
}



}
