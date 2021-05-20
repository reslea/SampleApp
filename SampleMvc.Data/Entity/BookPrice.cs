namespace SampleMvc.Data.Entity
{
    public class BookPrice : BaseEntity
    {
        //[Required]
        public Book Book { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }
    }
}