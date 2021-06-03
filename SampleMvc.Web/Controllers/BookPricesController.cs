using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Data;
using SampleMvc.Data.Entity;

namespace SampleMvc.Web.Controllers
{
    public class BookPricesController : Controller
    {
        private readonly LibraryContext _context;

        public BookPricesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookPrices
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.BookPrices.Include(b => b.Book);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BookPrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPrice = await _context.BookPrices
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookPrice == null)
            {
                return NotFound();
            }

            return View(bookPrice);
        }

        // GET: BookPrices/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
            return View();
        }

        // POST: BookPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Price,Id")] BookPrice bookPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookPrice.BookId);
            return View(bookPrice);
        }

        // GET: BookPrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPrice = await _context.BookPrices.FindAsync(id);
            if (bookPrice == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookPrice.BookId);
            return View(bookPrice);
        }

        // POST: BookPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Price,Id")] BookPrice bookPrice)
        {
            if (id != bookPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookPriceExists(bookPrice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookPrice.BookId);
            return View(bookPrice);
        }

        // GET: BookPrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPrice = await _context.BookPrices
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookPrice == null)
            {
                return NotFound();
            }

            return View(bookPrice);
        }

        // POST: BookPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookPrice = await _context.BookPrices.FindAsync(id);
            _context.BookPrices.Remove(bookPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookPriceExists(int id)
        {
            return _context.BookPrices.Any(e => e.Id == id);
        }
    }
}
