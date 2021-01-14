using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class AdministrationForm
    {
        public AdministrationForm()
        {
            Products = new HashSet<Product>();
        }

        public int IdAdministrationForm { get; set; }
        public string AdministrationName { get; set; }
        public string AdministrationVia { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
