using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleService.Domain.Helpers;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Mapper_Configuration
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  Mapping when property names are different
            CreateMap<CustomerVehicle, CustomerVehiclesDTO>()
                .ForMember(dest =>
                dest.VIN,
                opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest =>
                dest.LastModificationStatus,
                opt => opt.MapFrom(src => src.LastStatusModificationTime))
                .ForMember(dest => dest.Customer,
                           opt => opt.MapFrom(i => new CustomersDTO(i.Customer)));

            CreateMap<PagedList<CustomerVehicle>,PagedList<CustomerVehiclesDTO>>()
               .ConvertUsing<PagedListConverter<CustomerVehicle,CustomerVehiclesDTO>>();

            CreateMap<Customer, CustomersDTO>()
                         .ForMember(dest => dest.ID,
                         opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<CustomerVehicleChangedIntegrationEvent, CustomerVehicle>()
                 .ForMember(dest =>
                dest.VehicleId,
                opt => opt.MapFrom(src => src.VIN))
                .ForMember(dest =>
                dest.LastStatusModificationTime,
                opt => opt.MapFrom(src => src.ModificationStatus))
                .ForMember(dest =>
                dest.IsConnectedStatus,
                opt => opt.MapFrom(src => src.ConnectionStatus));



        }
    }
}
