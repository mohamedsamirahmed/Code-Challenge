using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.DTO
{
   public class CustomersDTO
    {
        public CustomersDTO()
        {

        }
        public CustomersDTO(Customer customer)
        {
            ID = customer.CustomerId;
            Name = customer.Name;
            Address = customer.Address;
            City = customer.City;
            PostalCode = customer.PostalCode;
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        //public static List<CustomersDTO> MapFields(IQueryable<Customer> customerLst)
        //{
        //    List<CustomersDTO> customerDtoLst = new List<CustomersDTO>();
        //    foreach (Customer cus in customerLst)
        //    {
        //        customerDtoLst.Add(new CustomersDTO(cus));
        //    }
        //    return customerDtoLst.ToList();
        //}

    }
}
