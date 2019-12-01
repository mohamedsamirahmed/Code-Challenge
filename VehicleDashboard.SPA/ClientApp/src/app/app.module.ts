import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core'; 
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router'; 
import { AppComponent } from './app.component'; 
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CustomerVehicleDashboardComponent } from './customer-vehicle-dashboard/customer-vehicle-dashboard.component';
import { CustomerVehicleDetailComponent } from './customer-vehicle-details/customer-vehicle-details.component';
import { CustomerVehicleDashboardService } from './services/customer-vehicle-dashboard.service';
import { AlertifyService } from './Shared/alertify.service';
  
       
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CustomerVehicleDashboardComponent,
    CustomerVehicleDetailComponent
  ], 
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    RouterModule.forRoot([
      {
        path: '', component: CustomerVehicleDashboardComponent },
      { path: 'customerVehicleHistory/:id/:vin/:regNo', component: CustomerVehicleDetailComponent },
      { path: '**', redirectTo: '', pathMatch: 'full' }
    ]) 
  ],
  providers: [CustomerVehicleDashboardService, AlertifyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
