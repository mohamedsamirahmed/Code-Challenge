using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDashboard.Simulator.HostScheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class CustomerVehiclesHistoryJob : IJob
    {
        private readonly ILogger _logger;

        public CustomerVehiclesHistoryJob(ILogger<CustomerVehiclesHistoryJob> logger )
        {
            _logger = logger;
        }

        /// <summary>
        /// call service to add random history per each vehicle
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            //Random rnd = new Random();
            //_logger.LogWarning("test" + rnd.Next(0, 200).ToString());
            return Task.CompletedTask;
        }
    }
}
