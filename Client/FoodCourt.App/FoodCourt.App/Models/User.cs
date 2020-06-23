using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FoodCourt.Models
{
    public class User
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
