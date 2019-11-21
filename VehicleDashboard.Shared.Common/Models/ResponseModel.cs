using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VehicleDashboard.Shared.Common.Models
{
    public class ResponseModel<T>
    {
        public bool ReturnStatus { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable Errors;
        public int TotalPages;
        public int TotalRows;
        public int PageSize;
        public T Entity;

        public ResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
        }
    }
}
