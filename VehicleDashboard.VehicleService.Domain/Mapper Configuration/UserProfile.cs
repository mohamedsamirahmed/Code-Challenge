using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.VehicleService.Domain.Helpers;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Mapper_Configuration
{

  

    //public class PagedListConverter : ITypeConverter<PagedList<CustomerVehicle>, PagedList<CustomerVehiclesDTO>>
    //{
    //    public PagedList<CustomerVehiclesDTO> Convert(PagedList<CustomerVehicle> source, PagedList<CustomerVehiclesDTO> destination, ResolutionContext context)
    //    {
    //        var models = source;
    //        var viewModels = models.Select(p => Mapping.Mapper.Map<CustomerVehicle, CustomerVehiclesDTO>(p)).ToList();
    //        return new PagedList<CustomerVehiclesDTO>(viewModels, models.TotalCount, models.CurrentPage, models.PageSize);
    //    }
    //}

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Default mapping when property names are same
            //CreateMap<CustomerVehicle, CustomerVehiclesDTO>();


            //  Mapping when property names are different
            CreateMap<CustomerVehicle, CustomerVehiclesDTO>()
                .ForMember(dest =>
                dest.VIN,
                opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest =>
                dest.LastModificationStatus,
                opt => opt.MapFrom(src => src.LastStatusModificationTime))
                .ForMember(dest => dest.Customer,
                           input => input.MapFrom(i => new CustomersDTO(i.Customer)));

            CreateMap<PagedList<CustomerVehicle>,PagedList<CustomerVehiclesDTO>>()
               .ConvertUsing<PagedListConverter<CustomerVehicle,CustomerVehiclesDTO>>();

            CreateMap<Customer, CustomersDTO>()
                         .ForMember(dest =>
                         dest.ID,
                         opt => opt.MapFrom(src => src.CustomerId));
            
            //.ForMember(dest =>
            //dest.Customer.ID,
            //opt => opt.MapFrom(src => src.Customer.CustomerId))
            //.ForMember(dest =>
            //dest.Customer.Address,
            //opt => opt.MapFrom(src => src.Customer.Address))
            //.ForMember(dest =>
            //dest.Customer.City,
            //opt => opt.MapFrom(src => src.Customer.City))
            //.ForMember(dest =>
            //dest.Customer.Name,
            //opt => opt.MapFrom(src => src.Customer.Name))
            //.ForMember(dest =>
            //dest.Customer.PostalCode,
            //opt => opt.MapFrom(src => src.Customer.PostalCode))
            //.ForMember(dest => dest.Customer, opt => opt.Ignore())
            // ;


        }
    }
}
