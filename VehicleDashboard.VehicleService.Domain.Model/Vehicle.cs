using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleDashboard.VehicleService.Domain.Model
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public string VIN { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }

        public List<CustomerVehicle> CustomerVehicles { get; set; }

    }
}
