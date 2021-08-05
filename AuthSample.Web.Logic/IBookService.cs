using System.Collections.Generic;
using AuthSample.WebDb;

namespace AuthSample.Web.Logic
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        Book Get(int id);

        Book Create(Book book);

        void Update(Book book);

        void Delete(int id);
    }
}
