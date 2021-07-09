using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSample.Core;
using AuthSample.Web.Models;
using AuthSample.Web.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace AuthSample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public static List<BookModel> BookModels = new List<BookModel>
        {
            new BookModel
            {
                Author = "Дж. Роулинг",
                Title = "и Философский камень",
                PagesCount = 300,
                PublishDate = new DateTime(1997, 3,3)
            },
        };

        [HttpGet]
        [RequirePermission(PermissionType.ReadBooks)]
        public IActionResult GetAll()
        {
            return Ok(BookModels);
        }

        [HttpPost]
        [RequirePermission(PermissionType.EditBooks)]
        public IActionResult CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookModels.Add(model);

            return NoContent();
        }
    }
}
