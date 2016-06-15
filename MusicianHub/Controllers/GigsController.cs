using Microsoft.AspNet.Identity;
using MusicianHub.Models;
using MusicianHub.ViewModel;
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

        // GET: Gigs/Create
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            //ViewBag.Genres = new SelectList(_context.Genres.ToList().AsEnumerable(), "Id", "Name");
            return View(viewModel);
        }

        // POST: Gigs/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            try
            {
                //by default the Gig table/model have relation with Genre table and Artist
                //So instead of using the relation with calling all the class
                //we optimize the call to database and query with add and id for the two model in the Gig class 

                var gig = new Gig
                {
                    ArtistId = User.Identity.GetUserId(),
                    //because this too much details to the Controller
                    //DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}"),
                    DateTime = viewModel.GetDatetime(),
                    Venue = viewModel.Venue,
                    GenreId = viewModel.Genre

                };

                _context.Gigs.Add(gig);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
        }

    }
}
