using System;
using System.Collections.Generic;

#nullable disable

namespace FarmAppApi.Models
{
    public partial class Review
    {
        public int IdReview { get; set; }
        public string ReviewName { get; set; }
        public string ReviewDescription { get; set; }
    }
}
