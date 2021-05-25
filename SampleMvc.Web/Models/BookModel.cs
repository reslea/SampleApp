using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SampleMvc.Data.Entity;

namespace SampleMvc.Web.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Range(0, 10000)]
        public int PagesCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
