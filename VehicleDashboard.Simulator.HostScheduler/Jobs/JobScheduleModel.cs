using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDashboard.Simulator.HostScheduler.Jobs
{
    public class JobScheduleModel
    {
        public JobScheduleModel(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        //type of the job ex:CustomerVehiclesHistoryJob
        public Type JobType { get; }
        
        //Time elapse for the job
        public string CronExpression { get; }
    }
}
