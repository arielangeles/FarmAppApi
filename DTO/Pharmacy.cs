using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppApi.DTO
{
    public class Pharmacy
    {
        public Pharmacy() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PharmacyChain { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
