using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBus.Events;

namespace VehiclesDashboard.VehicleConnection.API.IntegrationEvents
{
    public class CustomerVehicleHistoryIntegrationEventService : ICustomerVehicleHistoryIntegrationEventService
    {
      //  private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
      //  private readonly CatalogContext _catalogContext;
        //private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<CustomerVehicleHistoryIntegrationEventService> _logger;

        public CustomerVehicleHistoryIntegrationEventService(
            ILogger<CustomerVehicleHistoryIntegrationEventService> logger,
            IEventBus eventBus
            //CatalogContext catalogContext,
            //Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           // _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
           // _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
           // _eventLogService = _integrationEventLogServiceFactory(_catalogContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                //await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                //await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
              //  await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        //public async Task SaveEventAndCustomerVehicleHistoryContextChangesAsync(IntegrationEvent evt)
        //{
        //    _logger.LogInformation("----- SaveEventAndCustomerVehicleHistoryContextChangesAsync - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

        //    await ResilientTransaction.New(_catalogContext).ExecuteAsync(async () =>
        //    {
        //        // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
        //        await _catalogContext.SaveChangesAsync();
        //        await _eventLogService.SaveEventAsync(evt, _catalogContext.Database.CurrentTransaction.GetDbTransaction());
        //    });
        //}
    }
}
