using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Simulator.HostScheduler.IntegrationEvents.Events;

namespace VehicleDashboard.Simulator.HostScheduler.Helpers
{
    public class SimulatorHelper
    {
        public List<CustomerVehicleChangedIntegrationEvent> GenerateRandomStatus()
        {
            return new List<CustomerVehicleChangedIntegrationEvent>() {
            new CustomerVehicleChangedIntegrationEvent("YS2R4X20005399401","ABC123",1,GetRandomBool(),DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent("VLUR4X20009093588","DEF456",1,GetRandomBool(),DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent("VLUR4X20009048066","GHI789",1, GetRandomBool(),DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent("YS2R4X20005388011","JKL012",2,GetRandomBool(),DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent("YS2R4X20005387949","MNO345",2, GetRandomBool(), DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent ("VLUR4X20009048066","PQR678",3, GetRandomBool(), DateTime.Now),
                new CustomerVehicleChangedIntegrationEvent ("YS2R4X20005387055","STU901",3,GetRandomBool(),DateTime.Now)
            };
        }

        private bool GetRandomBool()
        {
            Random rng = new Random();
            return rng.Next(0, 2) > 0;
        }
    }
}
