using System;
using Microsoft.AspNetCore.Mvc;
using AuthSample.Core;
using AuthSample.Web.Logic;
using AuthSample.Web.Models;
using AuthSample.Web.Utilities;
using AuthSample.WebDb;

namespace AuthSample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        [RequirePermission(PermissionType.ReadBooks)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        [RequirePermission(PermissionType.ReadBooks)]
        public IActionResult Get(int id)
        {
            var book = _service.Get(id);

            return Ok(book);
        }

        [HttpPost]
        [RequirePermission(PermissionType.EditBooks)]
        public IActionResult CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _service.Create(Map(model));

            return Ok(Map(created));
        }

        [HttpPut("{id}")]
        [RequirePermission(PermissionType.EditBooks)]
        public IActionResult UpdateBook(int id, BookModel model)
        {
            try
            {
                model.Id = id;
                _service.Update(Map(model));
            }
            catch (ArgumentException)
            {
                return NotFound($"Book with id {id} not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [RequirePermission(PermissionType.EditBooks)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound($"Book with id {id} not found");
            }

            return NoContent();
        }

        public static Book Map(BookModel model)
        {
            return new Book
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                PagesCount = model.PagesCount,
                PublishDate = model.PublishDate
            };
        }

        public static BookModel Map(Book model)
        {
            return new BookModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                PagesCount = model.PagesCount,
                PublishDate = model.PublishDate
            };
        }
    }
}
