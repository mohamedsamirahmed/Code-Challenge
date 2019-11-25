using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.DTO
{
    public class CustomerVehicleHistoryDTO
    {
        public CustomerVehicleHistoryDTO()
        {

        }
        public CustomerVehicleHistoryDTO(CustomerVehicleHistory customerVehicle)
        {
            VIN = customerVehicle.VehicleId;
            RegNo = customerVehicle.RegNo;
            CustomerId = customerVehicle.CustomerId;
            ConnectionStatus = customerVehicle.ConnectionStatus;
            ModificationStatus = customerVehicle.StatusModificationTime!=null?customerVehicle.StatusModificationTime:DateTime.Now;
        }

      //  [Required]
        public string VIN { get; set; }

      //  [Required]
        public string RegNo { get; set; }

     //   [Required]
        public int CustomerId { get; set; }

    //    [Required]
        public bool ConnectionStatus { get; set; }

        
        public DateTime ModificationStatus { get; set; }

        public static List<CustomerVehicleHistoryDTO> MapFields(IQueryable<CustomerVehicleHistory> customerVehicleHistoryList)
        {
            List<CustomerVehicleHistoryDTO> customerDtoLst = new List<CustomerVehicleHistoryDTO>();
            foreach (CustomerVehicleHistory customerVehicleHistory in customerVehicleHistoryList)
            {
                customerDtoLst.Add(new CustomerVehicleHistoryDTO(customerVehicleHistory));
            }
            return customerDtoLst.ToList();
        }

        public CustomerVehicleHistory GetEntity() {
            return new CustomerVehicleHistory()
            {
                ConnectionStatus = this.ConnectionStatus,
                CustomerId = this.CustomerId,
                RegNo = this.RegNo,
                StatusModificationTime = this.ModificationStatus,
                VehicleId = this.VIN
            };
        }

    }
}
