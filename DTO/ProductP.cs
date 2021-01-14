using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppApi.DTO
{
    public class ProductP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Mg { get; set; }
        public string AdministrationForm { get; set; }
        public string AdministrationVia { get; set; }
    }
}
