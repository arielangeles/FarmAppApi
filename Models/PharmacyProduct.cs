using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class PharmacyProduct
    {
        public int IdPharmacyProduct { get; set; }
        public int IdPharmacyBranch { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        public virtual PharmacyBranch IdPharmacyBranchNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
