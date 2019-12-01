using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.VehicleConnection.Domain.Mapper_Configuration;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Helpers
{
  public  class Utility
    {
        public CustomerVehicleHistory GetCustomerVehicleHistoryEntity( CustomerVehicleHistoryDTO customerVehicleHistoryDto) {
            return Mapping.Mapper.Map<CustomerVehicleHistoryDTO, CustomerVehicleHistory>(customerVehicleHistoryDto); ;
        }
    }
}
