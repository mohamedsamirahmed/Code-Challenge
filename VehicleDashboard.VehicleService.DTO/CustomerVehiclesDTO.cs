using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.DTO
{
    public class CustomerVehiclesDTO
    {
        public CustomerVehiclesDTO()
        {

        }
        public CustomerVehiclesDTO(CustomerVehicle customerVehicle)
        {
            VIN = customerVehicle.VehicleId;
            RegNo = customerVehicle.RegNo;
            customerId = customerVehicle.CustomerId;
            IsConnectedStatus = customerVehicle.IsConnectedStatus;
            LastModificationStatus = customerVehicle.LastStatusModificationTime;
            Customer = new CustomersDTO(customerVehicle.Customer);
        }
        public string VIN { get; set; }

        public string RegNo { get; set; }

        public int customerId { get; set; }

        public bool IsConnectedStatus { get; set; }

        public string CurrentStatus
        {
            get
            {
                return IsConnectedStatus ? "Connected" : "Disconnected";
            }
        }
        public DateTime LastModificationStatus { get; set; }

        public CustomersDTO Customer { get; set; }

        //public static List<CustomerVehiclesDTO> MapFields(IQueryable<CustomerVehicle> customerVehicleList)
        //{
        //    List<CustomerVehiclesDTO> customerDtoLst = new List<CustomerVehiclesDTO>();
            
        //    foreach (var customerVehicle in customerVehicleList)
        //    {
        //        customerDtoLst.Add(new CustomerVehiclesDTO(customerVehicle));
        //    }
        //    return customerDtoLst.ToList();
        //}

        public CustomerVehicle GetEntity()
        {
            return new CustomerVehicle()
            {
                IsConnectedStatus = this.IsConnectedStatus,
                CustomerId = this.customerId,
                RegNo = this.RegNo,
                LastStatusModificationTime = this.LastModificationStatus,
                VehicleId = this.VIN
            };
        }

    }
}
