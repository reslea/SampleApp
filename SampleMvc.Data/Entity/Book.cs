using System;

namespace SampleMvc.Data.Entity
{
    //[Table("Prices")]
    public class Book : BaseEntity
    {
        //[Required, StringLength(255)]
        public string Title { get; set; }
        
        //[Required, StringLength(255)]
        public string Author { get; set; }

        public Genre Genre { get; set; }

        public int PagesCount { get; set; }

        public DateTime PublishDate { get; set; }

        public BookPrice BookPrice { get; set; }
    }
}