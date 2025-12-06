using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4.Data;
using lab4.Models;

namespace lab4.Controllers
{
    public class booksController : Controller
    {
        private readonly lab4Context _context;

        public booksController(lab4Context context)
        {
            _context = context;
        }


        public async Task<IActionResult> search()
        {

            List<book> brItems = new List<book>();

            return View(brItems);

        }

        [HttpPost]
        public async Task<IActionResult> Search(string s)
        {
            var li = await _context.categories.ToListAsync();
            ViewBag.Catag = li;


            var bkItems = await _context.book.FromSqlRaw("select * from book where title LIKE '%" + s + "%' ").ToListAsync();
            return View(bkItems);
        }


        public async Task<IActionResult> orderdetails()
        {
            var orBooks = await _context.Orderdetail.FromSqlRaw("select myorders.Id as id, name as customer, title as booktitle, myorders.quantity as quantity from book, myorders, usersaccounts  where book.id = myorders.bookid and myorders.custid = usersaccounts.id  ").ToListAsync();
            return View(orBooks);
        }


        // GET: books
        public async Task<IActionResult> Index()
        {
            return View(await _context.book.ToListAsync());
        }

        public async Task<IActionResult> Catalog()
        {
            return View(await _context.book.ToListAsync());
        }

        // GET: books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            var book = await _context.book.FirstOrDefaultAsync(m => m.Id == id);


            return View(book);
        }

        // GET: books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("title,info,price,discount,pubdate,category,bookquantity")] book book)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.imgfile = filename;
            }


            _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
        }

        // GET: books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            var book = await _context.book.FindAsync(id);
            
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,title,info,price,discount,pubdate,category,bookquantity,imgfile")] book book)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.imgfile = filename;
            }


            _context.Update(book);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           

            var book = await _context.book
                .FirstOrDefaultAsync(m => m.Id == id);
          

            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.book.FindAsync(id);
            if (book != null)
            {
                _context.book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool bookExists(int id)
        {
            return _context.book.Any(e => e.Id == id);
        }
    }
}
