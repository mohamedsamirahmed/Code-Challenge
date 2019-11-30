using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.Core.Common.Helper;

namespace VehicleDashboard.VehicleService.Domain.Helpers
{
    public class CustomerVehicleParams : EntityParams
    {
        public string Customer { get; set; }
        public string Vehicle { get; set; }
    }
}
