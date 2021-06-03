using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleMvc.Data.Entity;

namespace SampleMvc.Web.Models
{
    public class FilterModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int? PagesCount { get; set; }

        public Genre? Genre{ get; set; }
    }
}
