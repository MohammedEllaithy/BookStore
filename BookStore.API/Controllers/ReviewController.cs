using BookStore.API.ViewModels;
using BookStore.Core.Entities;
using BookStore.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork<Review> _review;
        private readonly Tenant _tenant;

        public ReviewController(IUnitOfWork<Review> review, Tenant tenant)
        {
            _review = review;
            _tenant = tenant;

        }
        // GET: ReviewController
        public async Task<ActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tenantViewModel = new TenantViewModel
            {

                Reviews = _review.Entity.GetAll(e => e.TenantId == _tenant.Id).ToList()
            };
            return View(tenantViewModel);


        }
        // GET: ReviewController/Details/5
   
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _review.Entity.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _review.Entity.Add(review);
                    _review.save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(review);
        }


   
    }
}
