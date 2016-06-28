using Microsoft.AspNet.Identity;
using MusicianHub.Models;
using MusicianHub.ViewModel;
using Newtonsoft.Json;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending"
            };

            return View("Gigs", viewModel);
        }

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
        public ActionResult Create(GigFormViewModel viewModel, FormCollection form)
        {
            #region Captcha Region

            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            string secretKey = "6LdECCMTAAAAAI7hEkqNka4somS8rq8YvjxoV59x"; // change this
            string gRecaptchaResponse = form["g-recaptcha-response"];

            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;
            // send post data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            // validate the response from Google reCaptcha
            var captChaesponse = JsonConvert.DeserializeObject<reCaptchaResponse>(result);
            if (!captChaesponse.Success)
            {
                ViewBag.CaptchaErrorMessage = "Sorry, please validate the reCAPTCHA";
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            // go ahead and write code to validate username password against database


            #endregion

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
