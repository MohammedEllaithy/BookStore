using BookStore.API.ViewModels;
using BookStore.Core.Entities;
using BookStore.Core.Repository;
using BookStore.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    //[ApiController]
    // [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork<Book> _book;
        private readonly IUnitOfWork<Category> _category;
        private readonly IUnitOfWork<Author> _author;

        private readonly Tenant _tenant;


        public BookController(IUnitOfWork<Book> book, Tenant tenant, IUnitOfWork<Category> category
            , IUnitOfWork<Author> author)

        {
            _book = book;
            _tenant = tenant;
            _author = author;
            _category = category;
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tenantViewModel = new TenantViewModel
            {

                Books = _book.Entity.GetAll(e => e.TenantId == _tenant.Id).ToList()

            };
            return View(tenantViewModel);

        }

        // GET: Book/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _book.Entity.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        // GET: /Book/Create
        public ActionResult Create()
        {

            ViewBag.categories = _category.Entity.GetAll(c => c.TenantId == _tenant.Id).ToList();
            ViewBag.authors = _author.Entity.GetAll(c => c.TenantId == _tenant.Id).ToList();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _book.Entity.Add(book);
                    _book.save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(book);
        }
        // Get Book Details in Edit Page
        public async Task<ActionResult> Edit(Guid id)
        {
            Book Book = await _book.Entity.GetByIdAsync(id);
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _book.Entity.Update(book);
                    _book.save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(book);
        }

        // GET: /Book/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            Book Book = await _book.Entity.GetByIdAsync(id);
            return View(Book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            _book.Entity.Delete(id);
            _book.save();
            return RedirectToAction("Index");
        }

    }
}
