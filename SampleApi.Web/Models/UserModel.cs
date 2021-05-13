using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
