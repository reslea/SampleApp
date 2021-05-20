using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleMvc.Web.Models;

namespace SampleMvc.Web.Controllers
{
    public class BookController : Controller
    {
        private static readonly List<BookModel> BookModels = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "и Философский камень", Author = "Дж. Роулинг", PagesCount = 300 }
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(BookModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm] BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            BookModels.Add(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(BookModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePost([FromForm] BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Update), model);
            }
            BookModels.Add(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = BookModels.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                BookModels.Remove(book);
            }

            return RedirectToAction("Index");
        }
    }
}
