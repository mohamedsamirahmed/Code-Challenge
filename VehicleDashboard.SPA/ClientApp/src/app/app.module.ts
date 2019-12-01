import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core'; 
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router'; 
import { AppComponent } from './app.component'; 
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CustomerVehicleDashboardComponent } from './customer-vehicle-dashboard/customer-vehicle-dashboard.component';
import { CustomerVehicleDetailComponent } from './customer-vehicle-details/customer-vehicle-details.component';
import { CustomerVehicleDashboardService } from './services/customer-vehicle-dashboard.service';
import { AlertifyService } from './Shared/alertify.service';
  
       
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
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
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
  providers: [CustomerVehicleDashboardService, AlertifyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
