using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class Product
    {
        public Product()
        {
            PharmacyProducts = new HashSet<PharmacyProduct>();
        }

        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int? Mg { get; set; }
        public int IdAdministrationForm { get; set; }

        public virtual AdministrationForm IdAdministrationFormNavigation { get; set; }
        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<PharmacyProduct> PharmacyProducts { get; set; }
    }
}
