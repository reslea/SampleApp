using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleMvc.Data.Entity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }


}
