using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SampleMvc.Data;
using SampleMvc.Data.Entity;
using SampleMvc.Web.Models;
using SampleMvc.Web.Utilities;

namespace SampleMvc.Web.Controllers
{
    [EnsureAuthenticated]
    public class BookController : Controller
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Index)));
            
            var books = _context.Books;

            Debug.WriteLine("ACTION EXECUTION");

            var bookModels = books.Select(Map).ToList();
            return View(bookModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Add)));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BookModel model)
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Index)));
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            await _context.Books.AddAsync(new Book
            {
                Title = model.Title,
                Author = model.Author,
                PagesCount = model.PagesCount,
                PublishDate = model.PublishDate,
                Genre = model.Genre
            });

            await _context.SaveChangesAsync();

            //var session = HttpContext.Session;
                
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(BookModel model)
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Index)));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost([FromForm] BookModel model)
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Index)));
            if (!ModelState.IsValid)
            {
                return View(nameof(Update), model);
            }
            _context.Books.Update(new Book
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                PagesCount = model.PagesCount,
                PublishDate = model.PublishDate,
                Genre = model.Genre
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpContext.Session.Set("last request", Encoding.UTF8.GetBytes(nameof(Index)));
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Filter([FromQuery] FilterModel model)
        {
            var books = _context.Books
                .CoditionalWhere(model.Title != null, 
                    b => b.Title.Contains(model.Title))
                .CoditionalWhere(model.Author != null, 
                    b => b.Author.Contains(model.Author))
                .CoditionalWhere(model.PagesCount != null, 
                    b => b.PagesCount == model.PagesCount)
                .CoditionalWhere(model.Genre != null, 
                    b => b.Genre == model.Genre)
                .Select(Map)
                .ToList();

            return View(nameof(Index), books);
        }

        private BookModel Map(Book book)
        {
            return new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PagesCount = book.PagesCount,
                Genre = book.Genre
            };
        }
            
    }
}
