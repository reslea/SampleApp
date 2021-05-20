using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Web.Models
{
    public class UserReadModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
