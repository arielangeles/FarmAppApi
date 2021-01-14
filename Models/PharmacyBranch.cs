using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class PharmacyBranch
    {
        public PharmacyBranch()
        {
            PharmacyProducts = new HashSet<PharmacyProduct>();
        }

        public int IdPharmacyBranch { get; set; }
        public int IdPharmacyChain { get; set; }
        public string PharmacyName { get; set; }
        public string AddressName { get; set; }
        public string PhoneNumber { get; set; }
        public string Schedule { get; set; }
        public int IdTown { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public virtual PharmacyChain IdPharmacyChainNavigation { get; set; }
        public virtual Town IdTownNavigation { get; set; }
        public virtual ICollection<PharmacyProduct> PharmacyProducts { get; set; }
    }
}
