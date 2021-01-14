using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppApi.DTO
{
    public class ProductPharmacy
    {
        public ProductPharmacy() 
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Mg { get; set; }
        public string AdministrationForm { get; set; }
        public string AdministrationVia { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        

    }
}
