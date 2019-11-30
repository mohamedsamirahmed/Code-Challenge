using System;
using VehicleDashboard.EventBus.Events;

namespace VehicleDashboard.EventBusRabbitMQ.Events
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class CustomerVehicleChangedIntegrationEvent : IntegrationEvent
    {
        public string VIN { get; set; }

        public string RegNo { get; set; }

        public int CustomerId { get; set; }

        public bool ConnectionStatus { get; set; }


        public DateTime ModificationStatus { get; set; }
        public string CustomerName { get; set; }

        public CustomerVehicleChangedIntegrationEvent(string VIN, string RegNo, int CustomerId, bool ConnectionStatus, DateTime ModificationStatus,string customerName)
        {
            this.VIN = VIN;
            this.RegNo = RegNo;
            this.CustomerId = CustomerId;
            this.ModificationStatus = ModificationStatus;
            this.ConnectionStatus = ConnectionStatus;
            this.CustomerName = CustomerName;
        }
    }
}
