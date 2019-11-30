using AutoMapper;
using System.Linq;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.VehicleConnection.Domain.Mapper_Configuration;

namespace VehicleDashboard.VehicleConnection.Domain.Helpers
{
    public class PagedListConverter<T, D> : IPagedListConverter<PagedList<T>, PagedList<D>>
    {
        public PagedList<D> Convert(PagedList<T> source, PagedList<D> destination, ResolutionContext context)
        {
            var models = source;
            var viewModels = models.Select(p => Mapping.Mapper.Map<T, D>(p)).ToList();
            return new PagedList<D>(viewModels, models.TotalCount, models.CurrentPage, models.PageSize);
        }
    }
}
