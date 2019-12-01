import { Component, OnInit } from '@angular/core';
import { CustomerVehicle } from '../models/customer-vehicle';
import { Pagination } from '../models/Pagination';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';
import { Lookup } from '../models/lookup';
import { CustomerVehicleDashboardService } from '../services/customer-vehicle-dashboard.service';
import { AlertifyService } from '../Shared/alertify.service';

@Component({
  selector: 'app-customer-vehicle-dashboard',
  templateUrl: './customer-vehicle-dashboard.component.html'
})
export class CustomerVehicleDashboardComponent implements OnInit {

  public _customerVehicles: CustomerVehicle[];
  pagination: Pagination
  customerVehicleParams: any = {};
  public _customers: Lookup[];
  public _vehicles: Lookup[];

  vehicleServiceBaseUrl = environment.VehicleServiceApiEndpoint;
  customerServiceEndpoint = this.vehicleServiceBaseUrl + 'CustomerVehicles/GetCustomers';
  vehicleServiceEndpoint = this.vehicleServiceBaseUrl + 'CustomerVehicles/GetVehicles';

  constructor(private customerVehicleService: CustomerVehicleDashboardService, private route: ActivatedRoute,
    private alertify: AlertifyService) { }

  ngOnInit(): void {

    this.customerVehicleParams.Customer = "";
    this.customerVehicleParams.Vehicle = "";

    this.loadCustomerVehicles();

    //Get All Customer Vehicles from service
    this.loadCustomerLookup(this.customerServiceEndpoint);
    //Get all vehicles for lookup filter
    this.loadVehicleLookup(this.vehicleServiceEndpoint);
  }

  //reset filter component values
  resetFilters() {
    this.customerVehicleParams.Customer = "";
    this.customerVehicleParams.Vehicle = "";
    this.loadCustomerVehicles();

  }

  //on change pagining 
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadCustomerVehicles();
  }


  //get all current page customer vehicles
  loadCustomerVehicles() {
    let pageNumber = null;
    let itemsPerPage = null;
    if (this.pagination) {
      pageNumber = this.pagination.currentPage;
      itemsPerPage = this.pagination.itemsPerPage;
    }

    //Get All Customer Vehicles from service
    this.customerVehicleService.getcustomerVehiclesList(pageNumber, itemsPerPage, this.customerVehicleParams).subscribe((response: any) => {
      if (response.result.returnStatus) {
        this._customerVehicles = response.result.entity;
        this.pagination = response.pagination
      }
      else
        this.alertify.error(response.result.returnMessage);

    }, error => {
      this.alertify.error(error);
    });
  }

  //get all customers
  loadCustomerLookup(serviceEndPoint) {
    //Get all Customers for lookup filter
    this.customerVehicleService.getlookupList(serviceEndPoint).subscribe((response: any) => {
      if (response.returnStatus)
        this._customers = response.entity;
      else
        this.alertify.error(response.returnMessage);

    }, error => {
      this.alertify.error(error);
    });
  }

  //get all vehicles
  loadVehicleLookup(serviceEndPoint) {
    //Get all Customers for lookup filter
    this.customerVehicleService.getlookupList(serviceEndPoint).subscribe((response: any) => {
      if (response.returnStatus)
        this._vehicles = response.entity;
      else
        console.log(response.returnMessage);

    }, error => {
      console.log(error);
    });
  }

}
