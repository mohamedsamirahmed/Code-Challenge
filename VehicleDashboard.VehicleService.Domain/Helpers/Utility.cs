using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleConnection.Domain.Mapper_Configuration;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Helpers
{
    public static class  Utility
    {
        public static CustomerVehicle GetCustomerVehicleEntity(CustomerVehicleChangedIntegrationEvent customerVehicleEventMessage)
        {
            return Mapping.Mapper.Map<CustomerVehicleChangedIntegrationEvent, CustomerVehicle>(customerVehicleEventMessage); ;
        }

        public static CustomerVehiclesDTO GetCustomerVehicleDTO(CustomerVehicleChangedIntegrationEvent customerVehicleEventMessage)
        {
            return Mapping.Mapper.Map<CustomerVehicleChangedIntegrationEvent, CustomerVehiclesDTO>(customerVehicleEventMessage); ;
        }

        public static CustomersDTO GetCustomerDTO(Customer customer)
        {
            return Mapping.Mapper.Map<Customer, CustomersDTO>(customer); ;
        }
    }
}
