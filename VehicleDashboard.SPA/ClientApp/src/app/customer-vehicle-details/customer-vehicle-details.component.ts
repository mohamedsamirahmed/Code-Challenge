import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerVehicle } from '../models/customer-vehicle';
import { CustomerVehicleDashboardService } from '../customer-vehicle-dashboard/services/customer-vehicle-dashboard.service';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-customer-vehicle-detail',
  templateUrl: './customer-vehicle-details.component.html'
})

export class CustomerVehicleDetailComponent implements OnInit {

  public _cutomerVehicleHistoryList: CustomerVehicle[];

  constructor(private customerVehicleService: CustomerVehicleDashboardService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {


    this.route.queryParams.subscribe(
      (params: Params) => {

        console.log();

        if (!(this.route.snapshot.params['id']) && (this.route.snapshot.params['vin']) && (this.route.snapshot.params['regNo'])) {
          this.router.navigate(['/']);
        }

        this.loadCustomerVehicleDetails();

      }
    );

  }

  loadCustomerVehicleDetails() {
    let customerId = +this.route.snapshot.params['id'];
    let vehicleId = this.route.snapshot.params['vin'];
    let regNo = this.route.snapshot.params['regNo'];


    this.customerVehicleService.getcustomerVehicleDetails(customerId, vehicleId, regNo).subscribe((response: any) => {
      if (response.returnStatus)
        this._cutomerVehicleHistoryList = response.entity;
      else
        console.log(response.returnMessage);

    }, error => {
      console.log(error);
    });
  }
}
