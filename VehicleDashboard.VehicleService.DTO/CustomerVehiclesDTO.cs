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

    }
}
