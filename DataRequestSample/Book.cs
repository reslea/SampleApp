using System;
using System.Dynamic;

namespace DataRequestSample
{
    public class Book : BaseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PagesCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}