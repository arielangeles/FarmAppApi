﻿using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public int IdCountry { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
