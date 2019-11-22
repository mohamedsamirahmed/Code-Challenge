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
import { CustomerVehicleDashboardService } from './customer-vehicle-dashboard/services/customer-vehicle-dashboard.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    CustomerVehicleDashboardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: CustomerVehicleDashboardComponent },
      //{ path: 'customerVehicleHistory/:id/:vin', component: CustomerVehicleDetailsComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [CustomerVehicleDashboardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
