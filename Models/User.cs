using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public int IdPerson { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public virtual Person IdPersonNavigation { get; set; }
    }
}
