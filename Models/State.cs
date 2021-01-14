using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class State
    {
        public State()
        {
            Towns = new HashSet<Town>();
        }

        public int IdState { get; set; }
        public int IdCountry { get; set; }
        public string StateName { get; set; }

        public virtual Country IdCountryNavigation { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
