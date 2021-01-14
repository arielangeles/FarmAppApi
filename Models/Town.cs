using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class Town
    {
        public Town()
        {
            PharmacyBranches = new HashSet<PharmacyBranch>();
        }

        public int IdTown { get; set; }
        public int IdState { get; set; }
        public string TownName { get; set; }

        public virtual State IdStateNavigation { get; set; }
        public virtual ICollection<PharmacyBranch> PharmacyBranches { get; set; }
    }
}
