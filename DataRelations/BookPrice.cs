using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRelations
{
    public class BookPrice
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }
    }
}
