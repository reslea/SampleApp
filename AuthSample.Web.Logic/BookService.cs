using System;
using System.Collections.Generic;
using AuthSample.WebDb;

namespace AuthSample.Web.Logic
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book Get(int id)
        {
            return _repository.Get(id);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Update(Book book)
        {
            var toUpdate = _repository.Get(book.Id);
            if(toUpdate is null) throw new ArgumentException("not found");

            toUpdate.Title = book.Title;
            toUpdate.Author = book.Author;
            toUpdate.PagesCount = book.PagesCount;

            _repository.Update(toUpdate);
        }

        public void Delete(int id)
        {
            var toDelete = _repository.Get(id);

            if(toDelete is null) throw new ArgumentException("not found");

            _repository.Delete(toDelete);
        }
    }
}