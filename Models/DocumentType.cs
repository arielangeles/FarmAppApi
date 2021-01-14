using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            People = new HashSet<Person>();
        }

        public int IdDocumentType { get; set; }
        public string DocumentType1 { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
