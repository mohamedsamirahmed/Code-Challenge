using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.Domain.Repositories;
using VehicleDashboard.VehicleService.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleService.DTO;
using Microsoft.EntityFrameworkCore;
using VehicleDashboard.Core.Common.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VehicleDashboard.VehicleService.Domain.Helpers;
using AutoMapper;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.VehicleService.Domain.Mapper_Configuration;

namespace VehicleDashboard.VehicleService.Domain.Services.Implementation
{
    public class VehicleDashboardService : IVehicleDashboardService
    {
        public ICustomerVehiclesRepository customerVehiclesRepo
        {
            get
            {
                if (_customerVehiclesRepo == null)
                {
                    _customerVehiclesRepo = new CustomerVehiclesRepository(_dbContext);
                }
                return _customerVehiclesRepo;
            }
        }


        private readonly VehicleServiceDataContext _dbContext;
        private ICustomerVehiclesRepository _customerVehiclesRepo;
        private readonly ILogger<VehicleDashboardService> _logger;
        private readonly IMapper _mapper;


        public VehicleDashboardService(VehicleServiceDataContext dbContext,
           ILogger<VehicleDashboardService> logger,
           IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        /// <summary>
        /// get All Customer Vehicles with configured maxnumber of pages and items per page
        /// </summary>
        /// <param name="customerVehicleParams">paged list customer vehicles items.</param>
        /// <returns></returns>
        public async Task<PagedList<CustomerVehiclesDTO>> GetCustomerVehicleList(CustomerVehicleParams customerVehicleParams)
        {
            //ResponseModel<IEnumerable<CustomerVehiclesDTO>> returnResponse = new ResponseModel<IEnumerable<CustomerVehiclesDTO>>();
            //IQueryable<CustomerVehiclesDTO> customerVehiclesListDTO = null;

            PagedList<CustomerVehiclesDTO> pagedCustomerVehiclesDto = null;
            try
            {
                IQueryable<CustomerVehicle> customerVehicles = customerVehiclesRepo.GetAll().Include(c => c.Customer);
                var pagedCustomerVehicles = await PagedList<CustomerVehicle>.CreateAsync(customerVehicles, customerVehicleParams.PageNumber, customerVehicleParams.PageSize);
                pagedCustomerVehiclesDto = Mapping.Mapper.Map<PagedList<CustomerVehicle>, PagedList<CustomerVehiclesDTO>>(pagedCustomerVehicles);

                return pagedCustomerVehiclesDto;

            }
            catch (Exception ex)
            {
                //   returnResponse.ReturnStatus = false;
                // returnResponse.ReturnMessage.Add(ex.Message);

                _logger.LogError(ex.Message);
            }

            //  returnResponse.Entity = customerVehiclesListDTO;
            // return returnResponse;

            return pagedCustomerVehiclesDto;

        }


        public async Task UpdateCustomerVehicleStatus(CustomerVehiclesDTO customerVehiclesDto)
        {
            try
            {
                var CustomerVehicleEntity = customerVehiclesDto.GetEntity();
                customerVehiclesRepo.Update(CustomerVehicleEntity);
                await customerVehiclesRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }


    }

}
