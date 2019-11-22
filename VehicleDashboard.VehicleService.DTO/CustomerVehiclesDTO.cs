using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.DTO
{
    public class CustomerVehiclesDTO
    {
        public CustomerVehiclesDTO(CustomerVehicle customerVehicle)
        {
            VIN = customerVehicle.VehicleId;
            RegNo = customerVehicle.RegNo;
            customerId = customerVehicle.CustomerId;
            CurrentStatus = customerVehicle.IsConnectedStatus ? "Connected" : "Disconnected";
            LastModificationStatus = customerVehicle.LastStatusModificationTime;
            Customer = new CustomersDTO(customerVehicle.Customer);
        }
        public string VIN { get; set; }

        public string RegNo { get; set; }

        public int customerId { get; set; }

        public string CurrentStatus { get; set; }

        public DateTime LastModificationStatus { get; set; }

        public CustomersDTO Customer { get; set; }

        public static List<CustomerVehiclesDTO> MapFields(IQueryable<CustomerVehicle> customerVehicleList)
        {
            List<CustomerVehiclesDTO> customerDtoLst = new List<CustomerVehiclesDTO>();
            foreach (CustomerVehicle customerVehicle in customerVehicleList)
            {
                customerDtoLst.Add(new CustomerVehiclesDTO(customerVehicle));
            }
            return customerDtoLst.ToList();
        }

    }
}
