using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample.Web.Models
{
    public class BookModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }

        public int PagesCount { get; set; }
    }
}
