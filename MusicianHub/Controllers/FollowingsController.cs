using Microsoft.AspNet.Identity;
using MusicianHub.Dtos;
using MusicianHub.Models;
using System.Linq;
using System.Web.Http;

namespace MusicianHub.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }


        /*FormBody Cuz our gigId in body not in the URL*/

        /*Add in header:*/
        /*Content-Type:  application/json*/
        /*Link:  http://localhost:404/api/Followings */
        [HttpPost]
        public IHttpActionResult Follow(AttendancesDTO dto)
        {
            string userId = User.Identity.GetUserId();

            var exists = _context.Followings.Any(a => a.FolloweeId == userId && a.FolloweeId == dto.FolloweeId);
            if (exists)
            {
                return BadRequest("The Following Already Exists.");
            }
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
