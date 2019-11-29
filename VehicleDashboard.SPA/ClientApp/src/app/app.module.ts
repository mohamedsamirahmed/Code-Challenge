import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CustomerVehicleDashboardComponent } from './customer-vehicle-dashboard/customer-vehicle-dashboard.component';
import { CustomerVehicleDashboardService } from './customer-vehicle-dashboard/services/customer-vehicle-dashboard.service';
import { CustomerVehicleDetailComponent } from './customer-vehicle-details/customer-vehicle-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    CustomerVehicleDashboardComponent,
    CustomerVehicleDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    PaginationModule.forRoot(),
    RouterModule.forRoot([
      {
        path: '', component: CustomerVehicleDashboardComponent },
      { path: 'customerVehicleHistory/:id/:vin/:regNo', component: CustomerVehicleDetailComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: '**', redirectTo: '', pathMatch: 'full' }
    ])
  ],
  providers: [CustomerVehicleDashboardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
