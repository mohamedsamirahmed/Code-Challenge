using System;
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

        #region Constructor
        public VehicleDashboardService(VehicleServiceDataContext dbContext,
           ILogger<VehicleDashboardService> logger,
           IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Repositories Property
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

        public ICustomerRepository customerRepo
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new CustomerRepository(_dbContext);
                }
                return _customerRepo;
            }
        }

        public IVehicleRepository vehicleRepo
        {
            get
            {
                if (_vehicleRepo == null)
                {
                    _vehicleRepo = new VehicleRepository(_dbContext);
                }
                return _vehicleRepo;
            }
        }


        private ICustomerVehiclesRepository _customerVehiclesRepo;
        private ICustomerRepository _customerRepo;
        private IVehicleRepository _vehicleRepo;
        #endregion

        #region Property Declaration
        private readonly ILogger<VehicleDashboardService> _logger;
        private readonly IMapper _mapper;
        private readonly VehicleServiceDataContext _dbContext;
        #endregion

        #region Disposal object
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        #endregion


        #region public Operations
        /// <summary>
        /// get All Customer Vehicles with configured maxnumber of pages and items per page
        /// </summary>
        /// <param name="customerVehicleParams">paged list customer vehicles items.</param>
        /// <returns></returns>
        public async Task<ResponseModel<PagedList<CustomerVehiclesDTO>>> GetCustomerVehicleList(CustomerVehicleParams customerVehicleParams)
        {
            ResponseModel<PagedList<CustomerVehiclesDTO>> returnResponse = new ResponseModel<PagedList<CustomerVehiclesDTO>>();

            PagedList<CustomerVehiclesDTO> pagedCustomerVehiclesDto = null;
            try
            {
                IQueryable<CustomerVehicle> customerVehicles = customerVehiclesRepo.GetAll().Include(c => c.Customer);
                int customerId = 0;
                if (!string.IsNullOrEmpty(customerVehicleParams.Customer) && int.TryParse(customerVehicleParams.Customer, out customerId))
                {
                    customerVehicles = customerVehicles.Where(c => c.CustomerId == customerId);
                }

                if (!string.IsNullOrEmpty(customerVehicleParams.Vehicle))
                {
                    customerVehicles = customerVehicles.Where(c => c.VehicleId == customerVehicleParams.Vehicle);
                }

                var pagedCustomerVehicles = await PagedList<CustomerVehicle>.CreateAsync(customerVehicles, customerVehicleParams.PageNumber, customerVehicleParams.PageSize);
                pagedCustomerVehiclesDto = Mapping.Mapper.Map<PagedList<CustomerVehicle>, PagedList<CustomerVehiclesDTO>>(pagedCustomerVehicles);

                returnResponse.Entity = pagedCustomerVehiclesDto;
                returnResponse.ReturnStatus = true;
                return returnResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return returnResponse;
            }
        }

        /// <summary>
        /// get all customers as lookup
        /// </summary>
        /// <returns>lookup collecion with text and value</returns>
        public ResponseModel<IQueryable<LookupDTO>> GetCustomerLookup()
        {
            ResponseModel<IQueryable<LookupDTO>> returnResponse = new ResponseModel<IQueryable<LookupDTO>>();
            try
            {
                IQueryable<LookupDTO> customers = customerRepo.GetAll().Select(c => new LookupDTO() { text = c.Name, value = c.CustomerId.ToString() });
                returnResponse.Entity = customers;
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            return returnResponse;
        }

        /// <summary>
        /// get all vehicles as lookup
        /// </summary>
        /// <returns>lookup collecion with text and value</returns>
        public ResponseModel<IQueryable<LookupDTO>> GetVehicleLookup()
        {
            ResponseModel<IQueryable<LookupDTO>> returnResponse = new ResponseModel<IQueryable<LookupDTO>>();
            try
            {
                IQueryable<LookupDTO> vehicles = vehicleRepo.GetAll().Select(v => new LookupDTO() { text = v.VIN, value = v.VIN });
                returnResponse.Entity = vehicles;
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            return returnResponse;
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


        #endregion

    }

}
