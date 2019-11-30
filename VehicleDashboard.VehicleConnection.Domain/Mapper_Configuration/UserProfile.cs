using AutoMapper;
using VehicleDashboard.Core.Common.Helper;
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
        }
    }
}
