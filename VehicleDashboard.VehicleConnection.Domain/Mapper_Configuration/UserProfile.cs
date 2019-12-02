using AutoMapper;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Mapper_Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  Mapping when property names are different
            CreateMap<CustomerVehicleHistory, CustomerVehicleHistoryDTO>()
                .ForMember(dest =>
                dest.VIN,
                opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest =>
                dest.ModificationStatus,
                opt => opt.MapFrom(src => src.StatusModificationTime));

            CreateMap<PagedList<CustomerVehicleHistory>, PagedList<CustomerVehicleHistoryDTO>>()
               .ConvertUsing<PagedListConverter<CustomerVehicleHistory, CustomerVehicleHistoryDTO>>();

            CreateMap<CustomerVehicleHistoryDTO,CustomerVehicleHistory>()
                 .ForMember(dest =>
                dest.VehicleId,
                opt => opt.MapFrom(src => src.VIN))
                .ForMember(dest =>
                dest.StatusModificationTime,
                opt => opt.MapFrom(src => src.ModificationStatus));

            CreateMap<CustomerVehicleChangedIntegrationEvent, CustomerVehicleHistory>()
                 .ForMember(dest =>
                dest.VehicleId,
                opt => opt.MapFrom(src => src.VIN))
                .ForMember(dest =>
                dest.StatusModificationTime,
                opt => opt.MapFrom(src => src.ModificationStatus));
                
        }
    }
}
