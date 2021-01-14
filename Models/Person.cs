using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class Person
    {
        public Person()
        {
            Users = new HashSet<User>();
        }

        public int IdPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
