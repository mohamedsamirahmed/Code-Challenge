using System;
using System.Collections.Generic;

namespace VehicleDashboard.VehicleService.DTO
{
    public class CustomerVehiclesDTO
    {
        public int VIN { get; set; }

        public int RegNo { get; set; }

        public IEnumerable<CustomersDTO> Customers { get; set; }

        public Boolean CurrentStatus { get; set; }

        public DateTime LastModificationStatus { get; set; }
    }
}
