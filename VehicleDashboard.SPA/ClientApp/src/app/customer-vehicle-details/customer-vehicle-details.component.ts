import { Component, OnInit } from '@angular/core';
import { CustomerVehicle } from '../models/customer-vehicle';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Pagination } from '../models/Pagination';
import { CustomerVehicleDashboardService } from '../services/customer-vehicle-dashboard.service';
import { faFilm } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-customer-vehicle-detail',
  templateUrl: './customer-vehicle-details.component.html'
})

export class CustomerVehicleDetailComponent implements OnInit {
  filmIcon = faFilm;
  public _cutomerVehicleHistoryList: CustomerVehicle[];
  pagination: Pagination

  constructor(private customerVehicleService: CustomerVehicleDashboardService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.Startup();
  }

  Startup() {
    this.route.queryParams.subscribe(
      (params: Params) => {
        if (!(this.route.snapshot.params['id']) && (this.route.snapshot.params['vin']) && (this.route.snapshot.params['regNo'])) {
          this.router.navigate(['/']);
        }

        this.loadCustomerVehicleDetails();
      }
    );
  }
  //on change pagining 
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadCustomerVehicleDetails();
  }


  loadCustomerVehicleDetails() {
    let customerId = +this.route.snapshot.params['id'];
    let vehicleId = this.route.snapshot.params['vin'];
    let regNo = this.route.snapshot.params['regNo'];
    let pageNumber = null;
    let itemsPerPage = null;
    if (this.pagination) {
      pageNumber = this.pagination.currentPage;
      itemsPerPage = this.pagination.itemsPerPage;
    }

    this.customerVehicleService.getcustomerVehicleDetails(customerId, vehicleId, regNo,pageNumber, itemsPerPage).subscribe((response: any) => {
      this._cutomerVehicleHistoryList = response.result;
      this.pagination = response.pagination

      //if (response.returnStatus)
      //  this._cutomerVehicleHistoryList = response.entity;
      //else
      //  console.log(response.returnMessage);

    }, error => {
      console.log(error);
    });
  }
}
