using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDashboard.VehicleService.Domain.Model
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        public string PostalCode { get; set; }

        public List<CustomerVehicle> CustomerVehicles { get; set; }

    }
}
