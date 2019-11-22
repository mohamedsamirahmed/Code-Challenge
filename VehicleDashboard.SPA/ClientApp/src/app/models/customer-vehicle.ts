import { Customer } from "./customer";

export interface CustomerVehicle {
  vin: string,
  regNo: string,
  currentStatus: boolean,
  customerId: number,
  customerName: string,
  lastModificationStatus: Date,
  customer: Customer
}
