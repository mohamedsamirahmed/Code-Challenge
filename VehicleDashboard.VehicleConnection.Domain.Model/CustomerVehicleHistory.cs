using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDashboard.VehicleConnection.Domain.Model
{
    [Table("CustomerVehicleHistory")]
    public class CustomerVehicleHistory
    {
        [Key]
        public int HistoryId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string VehicleId { get; set; }

        [Required]
        public string RegNo { get; set; }

        [Required]
        public Boolean ConnectionStatus { get; set; }

        
        public DateTime StatusModificationTime { get; set; }

    }
}
