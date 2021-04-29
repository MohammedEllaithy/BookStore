using BookStore.API.ViewModels;
using BookStore.Core.Entities;
using BookStore.Core.Repository;
using BookStore.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
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
        private readonly Tenant _tenant;


        public BookController(IUnitOfWork<Book> book, Tenant tenant)
        {
            _book = book;
            _tenant = tenant;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
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
        // GET: /Book/Create
        public ActionResult Create()
        {
          
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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
        public async Task<ActionResult> Edit(int id)
        {
            Book Book = await _book.Entity.GetByIdAsync(id);
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _book.Entity.Update(Book);
                    _book.save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(Book);
        }

        // GET: /Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book Book = await _book.Entity.GetByIdAsync(id);
            return View(Book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Book Book = await _book.Entity.GetByIdAsync(id);
            _book.Entity.Delete(id);
            _book.save();
            return RedirectToAction("Index");
        }
        
    }
}
