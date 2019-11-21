using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDashboard.SPA.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
