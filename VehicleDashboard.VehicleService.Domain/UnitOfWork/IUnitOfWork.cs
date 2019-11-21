using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.VehicleService.Domain.Repositories;

namespace VehicleDashboard.VehicleService.Domain.UnitOfWork
{
    public interface IUnitOfWork
       : IDisposable
    {
    }
}
