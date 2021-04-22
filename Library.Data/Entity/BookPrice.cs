namespace Library.Data.Entity
{
    public class BookPrice
    {
        //[Key]
        public int Id { get; set; }
        
        //[Required]
        public Book Book { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }
    }
}