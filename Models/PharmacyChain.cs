using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class PharmacyChain
    {
        public PharmacyChain()
        {
            PharmacyBranches = new HashSet<PharmacyBranch>();
        }

        public int IdPharmacyChain { get; set; }
        public string PharmacyName { get; set; }
        public string AddressName { get; set; }
        public string Rnc { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string WebPage { get; set; }

        public virtual ICollection<PharmacyBranch> PharmacyBranches { get; set; }
    }
}
