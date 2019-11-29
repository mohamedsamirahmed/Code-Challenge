import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerVehicle } from '../models/customer-vehicle';
import { CustomerVehicleDashboardService } from './services/customer-vehicle-dashboard.service';

@Component({
  selector: 'app-customer-vehicle-dashboard',
  templateUrl: './customer-vehicle-dashboard.component.html'
})
export class CustomerVehicleDashboardComponent implements OnInit {


  public _cutomerVehicles: CustomerVehicle[];

  constructor(private customerVehicleService: CustomerVehicleDashboardService) { }

  ngOnInit(): void {
    this.loadCustomerVehicles();
  }

  loadCustomerVehicles() {
    this.customerVehicleService.getcustomerVehiclesList().subscribe((response:any) => {
      if (response.returnStatus)
        this._cutomerVehicles = response.entity;
      else
        console.log(response.returnMessage);

    }, error => {
      console.log(error);
    });
  }
}
