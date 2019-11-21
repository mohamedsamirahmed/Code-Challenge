using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleDashboard.VehicleService.Domain.Model
{
    [Table("CustomerVehicles")]
    public class CustomerVehicle
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public string RegNo { get; set; }

        [Required]
        public Boolean IsConnectedStatus { get; set; }

        [Required]
        public DateTime LastStatusModificationTime { get; set; }

       
       

    }
}
