using Microsoft.AspNet.Identity;
using MusicianHub.Models;
using MusicianHub.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MusicianHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Gigs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gigs/Create
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        // POST: Gigs/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            try
            {

                var gig = new Gig
                {
                    ArtistId = User.Identity.GetUserId(),
                    DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}"),
                    Venue = viewModel.Venue,
                    GenreId = viewModel.Genre

                };

                _context.Gigs.Add(gig);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gigs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gigs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gigs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gigs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
