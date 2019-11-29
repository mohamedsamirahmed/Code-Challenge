using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleDashboard.VehicleService.Domain.Helpers
{
    interface IPagedListConverter<S,D>: ITypeConverter<S,D>
    {
    }
}
