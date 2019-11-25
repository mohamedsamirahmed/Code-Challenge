using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Events;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehiclesDashboard.VehicleConnection.API.IntegrationEvents
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class CustomerVehicleChangedIntegrationEvent : IntegrationEvent
    {
        //public int CustomertId { get; private set; }
        //public int VehicleId { get; private set; }
        //public int RegNo { get; private set; }

        //public decimal ConnectionStatus { get; private set; }

        //public decimal ModificationStatus { get; private set; }


        public CustomerVehicleHistoryDTO customerVehicleHistoryDto { get; set; }

        public CustomerVehicleChangedIntegrationEvent(CustomerVehicleHistoryDTO customerVehicleHistoryDto)
        {
            this.customerVehicleHistoryDto = customerVehicleHistoryDto;
        }
    }
}
